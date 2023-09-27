using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    ///<summary>�X�N���[�����x</summary>
    [SerializeField] float _scrollSpeed = 1.0f;
    ///<summary>�w�i�̃X�v���C�g</summary>
    SpriteRenderer _sprite = default;
    /// <summary>�X�s�[�h����</summary>
    [SerializeField] GameSpeedController _gamespeedcontroller;
    float _speedrate = 1.0f;
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>(); // �w�i�̃X�v���C�g���擾
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
        _sprite.transform.Translate(-1 * speed * Time.deltaTime, 0f, 0f); // ���ɃX�N���[��
        if (_sprite.transform.position.x < -1 * _sprite.bounds.size.x)
        {
            _sprite.transform.Translate(2 * _sprite.bounds.size.x, 0f, 0f); // �w�i�����[�v
        }
    }
    private void ApplySpeedrate(float value)
    {
        _speedrate = value;
    }
}
