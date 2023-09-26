using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class ObstacleMove : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] private float _endPos;

    public void Instantiate(float mag)
    {
        _moveSpeed *= mag;
        Move();
    }

    public void Move()
    {
        transform.DOMoveX(
                _endPos,
                _moveSpeed
            ).SetEase(Ease.Linear)
            .OnComplete(() =>
                Destroy(this.gameObject)
            );
    }
}