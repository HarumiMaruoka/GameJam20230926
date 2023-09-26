using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] private float _endPos;

    private void Start()
    {
        Move();
    }

    public void Instantiate(float mag)
    {
        _moveSpeed *= mag;
    }

    public void Move()
    {
        transform.DOMoveX(
                _endPos,
                _moveSpeed
            ).SetEase(Ease.Linear)
            .OnComplete(() => Destroy(this.gameObject));
    }
}