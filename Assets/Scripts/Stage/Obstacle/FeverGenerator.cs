using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class FeverGenerator : MonoBehaviour
{
    [Header("宝")]
    [SerializeField] private GameObject[] _jwel;
    [SerializeField] private float _generateInterval;
    [SerializeField] private Vector2 _generatePos = new Vector2(10, 0);

    [SerializeField] private GameSpeedController _gameSpeedController;

    [SerializeField] private ScoreController _scoreController;
    private async void Start()
    {
        await GenerateLoop(this.GetCancellationTokenOnDestroy());
    }

    private async UniTask GenerateLoop(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_generateInterval), cancellationToken: token);
                if (GameStatusController.Current == GameStatus.Play)
                {
                    GenerateJwel();
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Cancel");
            }
        }
    }

    void GenerateJwel()
    {
        if (_scoreController.FeverEnabled)
        {
            Instantiate(_jwel[Random.Range(0, _jwel.Length)], _generatePos,
           Quaternion.identity);
        }

    }
}