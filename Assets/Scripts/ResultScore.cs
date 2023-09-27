// 日本語対応
using UnityEngine;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour
{
    [SerializeField]
    private ScoreController _scoreController;
    [SerializeField]
    private Text _text;

    public void ApplyScore()
    {
        _text.text = _scoreController.Score.ToString("00000");
    }
}