using Glib.InspectorExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : MonoBehaviour
{
    [SerializeField, TagName] string _playerTag;
    [SerializeField] ScoreController scoreController;
    //スコアコントローラーがついているオブジェクトをアタッチする
    [SerializeField] int _score = 0;
    //アイテムのスコアの値
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _playerTag)
        {
            scoreController.AddScore(_score);
        } //プレイヤーがアイテムにぶつかったときScoreControllerのAddScoreメソッドが呼ばれる
    }
}
