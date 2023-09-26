// 日本語対応
using Glib.InspectorExtension;
using UnityEngine;

// プレイヤーと何かが接触したときを判定する用のクラス。
public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField, TagName]
    private string _obstacleTag;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == _obstacleTag)
        {
            // ここに障害物と接触した時の処理を記述する。
        }
    }
}