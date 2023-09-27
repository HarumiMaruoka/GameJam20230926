using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    ///<summary>スクロール速度</summary>
    [SerializeField] float _scrollSpeed = 1.0f;
    /// <summary>スピード制御</summary>
    GameSpeedController _gamespeedcontroller;
    [SerializeField] private float _deadPos;
    float _speedrate = 1.0f;
    private void Awake()
    {
        _gamespeedcontroller = FindAnyObjectByType<GameSpeedController>();
    }
    private void OnEnable()
    {
        ApplySpeedrate(_gamespeedcontroller.CurrentSpeed);
        if (_gamespeedcontroller)
            _gamespeedcontroller.OnSpeedChanged += ApplySpeedrate;
    }
    private void OnDisable()
    {
        if (_gamespeedcontroller)
            _gamespeedcontroller.OnSpeedChanged -= ApplySpeedrate;
    }
    void Update()
    {
        var speed = _scrollSpeed * _speedrate;
        this.gameObject.transform.Translate(-1 * speed * Time.deltaTime, 0f, 0f); // 横にスクロール
        if (transform.position.x <= _deadPos)
        {
            Destroy(this.gameObject);
        }
    }
    private void ApplySpeedrate(float value)
    {
        _speedrate = value;
    }
}
