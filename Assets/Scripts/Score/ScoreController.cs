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
    int _totalScore; //スコアの合計
    public int Score => _totalScore;
    public bool FeverEnabled => _feverEnabled;
    public void AddScore(int value)
    {
        int _startScore = _totalScore;
        _totalScore += value;
        StartCoroutine(Enumerator(_startScore, _totalScore));
        //ItemCount();
    } //アイテムを取得した時に_totalScoreにスコアを追加する

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
                Debug.Log("終わった");
            });
    }

    IEnumerator Enumerator(int startScore, int endScore)
    {
        float startTime = Time.time;
        // 終了時間
        float endTime = startTime + _countScoreTimer;
        do
        {
            float timeRate = (Time.time - startTime) / _countScoreTimer;
            float updateValue = (float)((endScore - startScore) * timeRate + startScore);
            _scoreText.text = updateValue.ToString("00000"); // （"f0" の "0" は、小数点以下の桁数指定）
            yield return null;
        }
        while (Time.time < endTime);
        // 最終的な着地のスコア
        _scoreText.text = endScore.ToString("00000");
    }
}
