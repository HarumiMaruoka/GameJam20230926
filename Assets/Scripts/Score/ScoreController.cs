using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class ScoreController : MonoBehaviour
{
    [SerializeField] Text _scoreText;
    [SerializeField] float _countScoreTimer;
    [SerializeField] float _feverTime;
    [SerializeField] int _happenFeverCount;
    [SerializeField] Slider _feverSlider;
    bool _feverEnabled = false;
    float _scoreItemCount;
    int _totalScore; //�X�R�A�̍��v
    public int Score => _totalScore;
    public bool FeverEnabled => _feverEnabled;
    public void AddScore(int value)
    {
        int _startScore = _totalScore;
        _totalScore += value;
        StartCoroutine(Enumerator(_startScore, _totalScore));
        //ItemCount();
    } //�A�C�e�����擾��������_totalScore�ɃX�R�A��ǉ�����

    public void ItemCount()
    {
        if (_feverEnabled) return;
        _scoreItemCount++;
        //_feverSlider.value = _scoreItemCount / _happenFeverCount;
        _feverSlider.DOValue(_scoreItemCount / _happenFeverCount, 1f);
        if (_scoreItemCount >= _happenFeverCount)
        {
            _feverEnabled = true;
            Invoke(nameof(StartFever), 1f);
            _scoreItemCount = 0;
        }
    }
    void StartFever()
    {
        _feverSlider.DOValue(0, _feverTime)
            .OnComplete(() =>
            {
                _feverEnabled = false;
                Debug.Log("�I�����");
            });
    }

    IEnumerator Enumerator(int startScore, int endScore)
    {
        float startTime = Time.time;
        // �I������
        float endTime = startTime + _countScoreTimer;
        do
        {
            float timeRate = (Time.time - startTime) / _countScoreTimer;
            float updateValue = (float)((endScore - startScore) * timeRate + startScore);
            _scoreText.text = updateValue.ToString("00000"); // �i"f0" �� "0" �́A�����_�ȉ��̌����w��j
            yield return null;
        }
        while (Time.time < endTime);
        // �ŏI�I�Ȓ��n�̃X�R�A
        _scoreText.text = endScore.ToString("00000");
    }
}
