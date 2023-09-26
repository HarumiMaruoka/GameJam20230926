using Glib.InspectorExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : MonoBehaviour
{
    [SerializeField, TagName] string _playerTag;
    ScoreController ScoreController;
    //スコアコントローラーがついているオブジェクトをアタッチする
    [SerializeField] int _score = 0;
    //アイテムのスコアの値

    void Start()
    {
        ScoreController = FindObjectOfType<ScoreController>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _playerTag)
        {
            ScoreController.AddScore(_score);
            Destroy(this.gameObject);
            if (this.gameObject.tag == "FeverItem") return;
            ScoreController.ItemCount();
        } //プレイヤーがアイテムにぶつかったときScoreControllerのAddScoreメソッドが呼ばれる
    }
}
