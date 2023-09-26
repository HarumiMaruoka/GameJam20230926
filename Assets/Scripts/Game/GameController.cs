// 日本語対応
using DG.Tweening;
using Glib.InspectorExtension;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField, InputName]
    private string _spaceInputName;
    [SerializeField, SceneName]
    private string _titleSceneName;
    [SerializeField]
    private PlayerCollisionHandler _collisionHandler;
    [SerializeField]
    private Image _fadeImage;
    [SerializeField]
    private float _fadeTime;

    [SerializeField]
    private Text _currentGameStatusText;

    private void OnEnable()
    {
        GameStatusController.OnStatusChanged += ApllyCurrentGameStatusText;
        if (_collisionHandler)
            _collisionHandler.OnHitObstacle += GameOver;
    }

    private void OnDisable()
    {
        GameStatusController.OnStatusChanged -= ApllyCurrentGameStatusText;
        if (_collisionHandler)
            _collisionHandler.OnHitObstacle -= GameOver;
    }

    private void Start()
    {
        // ゲームシーン開始時の処理。
        GameStatusController.ChangeGameStatus(GameStatus.StartPerformance);
        ApllyCurrentGameStatusText(GameStatusController.Current);
        _fadeImage.gameObject.SetActive(true);
        FadeOut(() => _fadeImage.gameObject.SetActive(false));
    }

    private void Update()
    {
        // ゲームステータスがEndでSpaceが押されたらフェードインしてタイトルシーンに遷移する。
        if (GameStatusController.Current == GameStatus.End &&
            Input.GetButtonDown(_spaceInputName))
        {
            _fadeImage.gameObject.SetActive(true);
            FadeIn(() => SceneManager.LoadScene(_titleSceneName));
            return;
        }

        // ゲームプレイ中以外の時スペースキーで状態遷移。
        if (Input.GetButtonDown(_spaceInputName))
        {
            var currentStatus = GameStatusController.Current;

            if (currentStatus != GameStatus.Play)
            {
                Step();
                return;
            }
        }
    }

    public void OnDestroy()
    {
        DOTween.KillAll();
    }

    private void FadeIn(Action onComplete)
    {
        _fadeImage.DOFade(1f, _fadeTime).OnComplete(() => onComplete?.Invoke());
    }
    private void FadeOut(Action onComplete)
    {
        _fadeImage.DOFade(0f, _fadeTime).OnComplete(() => onComplete?.Invoke());
    }

    private void Step()
    {
        var currentStatus = GameStatusController.Current;

        // None→ StartPerformance→ Play→ EndPerformance→ End→ None ...という流れで進行する。
        switch (currentStatus)
        {
            case GameStatus.None:
                GameStatusController.ChangeGameStatus(GameStatus.StartPerformance);
                break;
            case GameStatus.StartPerformance:
                GameStatusController.ChangeGameStatus(GameStatus.Play);
                break;
            case GameStatus.Play:
                GameStatusController.ChangeGameStatus(GameStatus.EndPerformance);
                break;
            case GameStatus.EndPerformance:
                GameStatusController.ChangeGameStatus(GameStatus.End);
                break;
            case GameStatus.End:
                GameStatusController.ChangeGameStatus(GameStatus.None);
                break;
        }
    }

    private void ApllyCurrentGameStatusText(GameStatus status)
    {
        if (_currentGameStatusText)
        {
            _currentGameStatusText.text = status.ToString();
        }
    }

    private void GameOver()
    {
        var currentStatus = GameStatusController.Current;
        if (currentStatus == GameStatus.Play)
        {
            Step();
        }
    }
}