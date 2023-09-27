// 日本語対応
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerDeadPerformance : MonoBehaviour
{
    [SerializeField]
    private float _jumpingPower = 8f;
    [SerializeField]
    private float _endYPos = -5.5f;

    private Rigidbody2D _rigidbody2D = null;

    // プレイヤーがやられたときの演出。
    public async void PlayDeadPerformance(Action onComplete = null)
    {
        try
        {
            _rigidbody2D ??= GetComponent<Rigidbody2D>();
            _rigidbody2D.velocity = new Vector2(0f, _jumpingPower);
            await UniTask.WaitUntil(() => transform.position.y < _endYPos,
                cancellationToken: this.GetCancellationTokenOnDestroy());
            onComplete?.Invoke();
            Destroy(this);
        }
        catch (OperationCanceledException)
        {
            Debug.Log("Canceled");
        }
    }
}