using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ObstacleGenerator : MonoBehaviour
{
    [Header("障害物")]
    [SerializeField] private ObstacleMove[] _obstacles;
    [SerializeField] private float _generateInterval;
    [SerializeField] private Vector2 _generatePos = new Vector2(10, 0);

    [SerializeField] private GameSpeedController _gameSpeedController;


    private async void Start()
    {
        await GenerateLoop();
    }

    private async UniTask GenerateLoop()
    {
        while (true)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_generateInterval));
            if (GameStatusController.Current == GameStatus.Play)
            {
                GenerateObstacle();
            }
        }
    }

    void GenerateObstacle()
    {
        ObstacleMove ob = Instantiate(_obstacles[Random.Range(0, _obstacles.Length)], _generatePos,
            Quaternion.identity);
        ob.Instantiate(_gameSpeedController.CurrentSpeed);
    }
}