using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleGenerator : MonoBehaviour
{
    [Header("障害物")]
    [SerializeField] private GameObject[] _obstacles;
    [SerializeField] private float _generateInterval;
    [SerializeField] private Vector2 _generatePos = new Vector2(10, 0);

    [SerializeField] private GameSpeedController _gameSpeedController;

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
                    GenerateObstacle();
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Cancel");
            }
        }
    }

    void GenerateObstacle()
    {
        ObstacleMove ob = Instantiate(_obstacles[Random.Range(0, _obstacles.Length)], _generatePos,
            Quaternion.identity).GetComponent<ObstacleMove>();
        ob.Instantiate(_gameSpeedController.CurrentSpeed);
    }
}