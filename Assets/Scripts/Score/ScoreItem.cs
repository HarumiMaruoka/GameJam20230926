using Glib.InspectorExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : MonoBehaviour
{
    [SerializeField, TagName] string _playerTag;
    ScoreController scoreController;
    //�X�R�A�R���g���[���[�����Ă���I�u�W�F�N�g���A�^�b�`����
    [SerializeField] int _score = 0;
    //�A�C�e���̃X�R�A�̒l

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
        } //�v���C���[���A�C�e���ɂԂ������Ƃ�ScoreController��AddScore���\�b�h���Ă΂��
    }
}
