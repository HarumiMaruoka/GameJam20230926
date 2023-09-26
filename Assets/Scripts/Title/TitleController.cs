// 日本語対応
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Glib.InspectorExtension;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    [SerializeField, SceneName]
    private string _gameSceneName;
    [SerializeField]
    private Image _fadeImage;
    [SerializeField] private float _fadeTime;

    private void Start()
    {
        // タイトルシーン開始時の処理をここに記述する。
        _fadeImage.gameObject.SetActive(true);
        FadeOut(() => _fadeImage.gameObject.SetActive(false));
    }

    public async void Step()
    {
        // フェードインしてゲームシーンへ遷移する処理をここに記述する。
        _fadeImage?.gameObject.SetActive(true);
        await FadeIn(LoadGameScene);
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene(_gameSceneName);
    }
    private async UniTask FadeIn(Action onComplete)
    {
        await _fadeImage.DOFade(1f, _fadeTime).OnComplete(() => onComplete?.Invoke()).
            ToUniTask(cancellationToken: this.GetCancellationTokenOnDestroy());
    }
    private async void FadeOut(Action onComplete)
    {
        await _fadeImage.DOFade(0f, _fadeTime).OnComplete(() => onComplete?.Invoke()).
            ToUniTask(cancellationToken: this.GetCancellationTokenOnDestroy());
    }
}