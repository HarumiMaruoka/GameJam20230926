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
    [SerializeField]
    private PlayerCollisionHandler _playerCollisionHandler;

    private void OnEnable()
    {
        GameStatusController.OnStatusChanged += ApllyCurrentGameStatusText;
        if (_collisionHandler)
            _collisionHandler.OnHitObstacle += GameOver;
        if (_playerCollisionHandler)
            _playerCollisionHandler.OnHitObstacle += PlayEndPerformance;
    }

    private void OnDisable()
    {
        GameStatusController.OnStatusChanged -= ApllyCurrentGameStatusText;
        if (_collisionHandler)
            _collisionHandler.OnHitObstacle -= GameOver;
        if (_playerCollisionHandler)
            _playerCollisionHandler.OnHitObstacle -= PlayEndPerformance;
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

    private float _startPerformanceTimer = 0f;
    private float _startTime = 3f;
    [SerializeField]
    private Text _startPerformanceTimeText;
    private async void PlayStartPerformance(Action onComplete = null) // 3.2.1のカウントダウンを表示する。
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
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private PlayerDeadPerformance _playerDeadPerformancePrefab;
    [SerializeField]
    private ResultDrawer _resultDrawer;
    [SerializeField]
    private ResultScore _resultScore;
    private void PlayEndPerformance()
    {
        GameStatusController.ChangeGameStatus(GameStatus.EndPerformance);
        var deadPos = _player.transform.position;
        var dead = Instantiate(_playerDeadPerformancePrefab, deadPos, Quaternion.identity);
        Destroy(_player);
        _resultScore.ApplyScore();
        dead.PlayDeadPerformance(() => // プレイヤーの死亡演出。
        _resultDrawer.Play(() => // 演出完了時、リザルト表示演出。
        GameStatusController.ChangeGameStatus(GameStatus.End))); // 演出完了時、ステート遷移。
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