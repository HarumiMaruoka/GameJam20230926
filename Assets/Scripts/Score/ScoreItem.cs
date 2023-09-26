using Glib.InspectorExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : MonoBehaviour
{
    [SerializeField, TagName] string _playerTag;
    ScoreController scoreController;
    //スコアコントローラーがついているオブジェクトをアタッチする
    [SerializeField] int _score = 0;
    //アイテムのスコアの値

    void Start()
    {
        scoreController = FindObjectOfType<ScoreController>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _playerTag)
        {
            scoreController.AddScore(_score);
            Destroy(this.gameObject);
        } //プレイヤーがアイテムにぶつかったときScoreControllerのAddScoreメソッドが呼ばれる
    }
}
