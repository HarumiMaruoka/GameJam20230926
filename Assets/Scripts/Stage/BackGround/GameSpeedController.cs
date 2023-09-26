using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedController : MonoBehaviour
{
    [SerializeField]
    float _currentSpeed = 1.0f; // 現在のスピード
    public float CurrentSpeed => _currentSpeed;
    public event Action<float> OnSpeedChanged;

    private void OnValidate()
    {
        OnSpeedChanged?.Invoke(_currentSpeed);
    }
    public void ChangeSpeed(float speed)
    {
        var old = _currentSpeed;
        _currentSpeed = speed;
        if (old != _currentSpeed)
        {
            OnSpeedChanged?.Invoke(_currentSpeed);
        }
    }
}
