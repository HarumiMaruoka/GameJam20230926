using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    ///<summary>スクロール速度</summary>
    [SerializeField] float _scrollSpeed = 1.0f;
    ///<summary>背景のスプライト</summary>
    SpriteRenderer _sprite = default;
    /// <summary>スピード制御</summary>
    [SerializeField] GameSpeedController _gamespeedcontroller;
    float _speedrate = 1.0f;
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>(); // 背景のスプライトを取得
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
        _sprite.transform.Translate(-1 * speed * Time.deltaTime, 0f, 0f); // 横にスクロール
        if (_sprite.transform.position.x < -1 * _sprite.bounds.size.x)
        {
            _sprite.transform.Translate(2 * _sprite.bounds.size.x, 0f, 0f); // 背景をループ
        }
    }
    private void ApplySpeedrate(float value)
    {
        _speedrate = value;
    }
}
