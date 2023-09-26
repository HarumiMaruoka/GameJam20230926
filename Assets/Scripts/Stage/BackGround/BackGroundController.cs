using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController:MonoBehaviour
{
    ///<summary>スクロール速度</summary>
    [SerializeField] float _scrollSpeed = 1.0f;
    ///<summary>背景のスプライト</summary>
    SpriteRenderer _sprite = default;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>(); // 背景のスプライトを取得
    }
    void Update()
    {
        _sprite.transform.Translate(-1 * _scrollSpeed * Time.deltaTime, 0f, 0f); // 横にスクロール
        if(_sprite.transform.position.x < -1 * _sprite.bounds.size.x)
        {
            _sprite.transform.Translate(2 * _sprite.bounds.size.x, 0f, 0f); // 背景をループ
        }
    }
}
