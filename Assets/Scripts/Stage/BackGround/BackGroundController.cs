using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController:MonoBehaviour
{
    ///<summary>�X�N���[�����x</summary>
    [SerializeField] float _scrollSpeed = 1.0f;
    ///<summary>�w�i�̃X�v���C�g</summary>
    SpriteRenderer _sprite = default;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>(); // �w�i�̃X�v���C�g���擾
    }
    void Update()
    {
        _sprite.transform.Translate(-1 * _scrollSpeed * Time.deltaTime, 0f, 0f); // ���ɃX�N���[��
        if(_sprite.transform.position.x < -1 * _sprite.bounds.size.x)
        {
            _sprite.transform.Translate(2 * _sprite.bounds.size.x, 0f, 0f); // �w�i�����[�v
        }
    }
}
