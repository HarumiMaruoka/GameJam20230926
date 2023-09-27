using Glib.InspectorExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : MonoBehaviour
{
    [SerializeField, TagName] string _playerTag;
    [SerializeField] int _score = 0;
    //アイテムのスコアの値
    ScoreController ScoreController;
    void Start()
    {
        ScoreController = FindObjectOfType<ScoreController>();
        if (this.gameObject.tag == "FeverItem")
        {
            GetComponent<Renderer>().material.color = Color.green;
            return;
        }
        int _randam = Random.Range(0, 3);
        if (_randam == 0) GetComponent<Renderer>().material.color = Color.red;
        else if(_randam == 1) GetComponent<Renderer>().material.color = Color.blue;
        else GetComponent<Renderer>().material.color = Color.yellow;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _playerTag)
        {
            ScoreController.AddScore(_score);
            Destroy(this.gameObject);
            if (this.gameObject.tag == "FeverItem") return;
            ScoreController.ItemCount();
        } //プレイヤーがアイテムにぶつかったときScoreControllerのAddScore ItemCountメソッドが呼ばれる
    }
}
