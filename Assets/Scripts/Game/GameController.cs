// 日本語対応
using Cysharp.Threading.Tasks;
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

        PlayStartPerformance(() => GameStatusController.ChangeGameStatus(GameStatus.Play));
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

        if (Input.GetButtonDown(_spaceInputName))
        {
            var currentStatus = GameStatusController.Current;

            if (currentStatus == GameStatus.EndPerformance)
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

    private float _startPerformanceTimer = 0f;
    private float _startTime = 3f;
    [SerializeField]
    private Text _startPerformanceTimeText;
    private async void PlayStartPerformance(Action onComplete = null)
    {
        _startPerformanceTimeText.gameObject.SetActive(true);
        _startPerformanceTimer = _startTime;

        while (_startPerformanceTimer >= 0f)
        {
            try
            {
                _startPerformanceTimer -= Time.deltaTime;
                _startPerformanceTimeText.text = _startPerformanceTimer.ToString("0.0");
                await UniTask.Yield(this.GetCancellationTokenOnDestroy());
            }
            catch (OperationCanceledException)
            {
                return;
            }
        }

        _startPerformanceTimeText.gameObject.SetActive(false);
        onComplete?.Invoke();
    }
    private void PlayEndPerformance(Action onComplete = null)
    {

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
            GameStatusController.ChangeGameStatus(GameStatus.EndPerformance);
        }
    }
}