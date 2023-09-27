// 日本語対応
using DG.Tweening;
using Glib.InspectorExtension;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    [SerializeField, InputName]
    private string _spaceInputName;
    [SerializeField, SceneName]
    private string _gameSceneName;
    [SerializeField]
    private Image _fadeImage;
    [SerializeField] private float _fadeTime;

    [SerializeField] private Animator _startAnim;
    [SerializeField] private AudioSource _startSE;
    
    private void Start()
    {
        // タイトルシーン開始時の処理をここに記述する。
        GameStatusController.ChangeGameStatus(GameStatus.None);
        _fadeImage.gameObject.SetActive(true);
        FadeOut(() => _fadeImage.gameObject.SetActive(false));
    }
    private void OnDestroy()
    {
        DOTween.KillAll();
    }

    private void Update()
    {
        if (Input.GetButtonDown(_spaceInputName))
        {
            _startSE.Play();
            _startAnim.SetBool("IsStart",true);
        }
    }

    public void Step()
    {
        // フェードインしてゲームシーンへ遷移する。
        _fadeImage?.gameObject.SetActive(true);
        FadeIn(LoadGameScene);
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene(_gameSceneName);
    }
    private void FadeIn(Action onComplete)
    {
        _fadeImage.DOFade(1f, _fadeTime).OnComplete(() => onComplete?.Invoke());
    }
    private void FadeOut(Action onComplete)
    {
        _fadeImage.DOFade(0f, _fadeTime).OnComplete(() => onComplete?.Invoke());
    }
}