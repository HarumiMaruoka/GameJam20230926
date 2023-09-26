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
    int _totalScore; //スコアの合計
    public int Score => _totalScore;
    public void AddScore(int value)
    {
        int _startScore = _totalScore;
        _totalScore += value;
        StartCoroutine(Enumerator(_startScore, _totalScore));
        //_totalScore += value;
        //_scoreText.text = _totalScore.ToString("00000");
    } //アイテムを取得した時に_totalScoreにスコアを追加する

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
