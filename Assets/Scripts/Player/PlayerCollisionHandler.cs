// 日本語対応
using Glib.InspectorExtension;
using System;
using UnityEngine;

// プレイヤーと何かが接触したときを判定する用のクラス。
public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField, TagName]
    private string _obstacleTag;

    public event Action OnHitObstacle;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == _obstacleTag)
        {
            // ここに障害物と接触した時の処理を記述する。
            OnHitObstacle?.Invoke();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _obstacleTag)
        {
            // ここに障害物と接触した時の処理を記述する。
            OnHitObstacle?.Invoke();
        }
    }
}