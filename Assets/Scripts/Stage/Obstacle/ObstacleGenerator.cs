using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] Obstacles;
    [SerializeField] private float _generateInterval;
    [SerializeField] private Vector2 _generatePos = new Vector2(10, 0);

    private void Start()
    {
        GenerateLoop();
    }

    private async UniTask GenerateLoop()
    {
        while (true)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_generateInterval));
            GenerateObstacle();
        }
    }

    void GenerateObstacle()
    {
        Instantiate(Obstacles[Random.Range(0,Obstacles.Length)],_generatePos, Quaternion.identity);
    }
}