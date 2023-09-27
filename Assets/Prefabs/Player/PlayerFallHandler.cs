// 日本語対応
using System;
using UnityEngine;

public class PlayerFallHandler : MonoBehaviour
{
    [SerializeField]
    private float _falledYPos = -5.5f; // 落下判定とする高さ。

    public event Action OnFalled;

    private void Update()
    {
        if (transform.position.y < _falledYPos)
        {
            OnFalled();
            GameObject.Destroy(gameObject);
        }
    }
}