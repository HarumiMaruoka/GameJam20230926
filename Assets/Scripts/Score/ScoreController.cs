using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

using System.Linq;

public class ScoreController : MonoBehaviour
{
    [SerializeField] Text _scoreText;
    [SerializeField] float _countScoreTimer;
    [SerializeField] float _feverTime;
    [SerializeField] int _happenFeverCount;
    [SerializeField] Slider _feverSlider;
    [SerializeField] private GameSpeedController _gameSpeedController;
    [SerializeField] private AudioSource _getSound;
    [SerializeField] private AudioSource _mainBGM;
    [SerializeField] private AudioSource _feverBGM;
    [SerializeField] private GameObject _fieldGenerator;
    [SerializeField] private GameObject _feverGenerator;
    [SerializeField] ParticleSystem[] _feverParticle;
    bool _feverEnabled = false;
    float _scoreItemCount;
    int _totalScore; //�X�R�A�̍��v
    public int Score => _totalScore;
    public bool FeverEnabled => _feverEnabled;
    public void AddScore(int value)
    {
        _getSound.Play();
        int _startScore = _totalScore;
        _totalScore += value;
        StartCoroutine(Enumerator(_startScore, _totalScore));
        //ItemCount();
    } //�A�C�e�����擾��������_totalScore�ɃX�R�A��ǉ�����

    public void ItemCount()
    {
        if (_feverEnabled) return;
        _scoreItemCount++;
        _feverSlider.DOValue(_scoreItemCount / _happenFeverCount, 1f);
        if (_scoreItemCount >= _happenFeverCount)
        {
            _feverEnabled = true;
            _feverGenerator.SetActive(true);
            _fieldGenerator.SetActive(false);
            _feverBGM.Play();
            _mainBGM.Stop();
            Invoke(nameof(StartFever), 1f);
            _scoreItemCount = 0;
        }
    }
    void StartFever()
    {
        foreach (var particle in _feverParticle)
        {
            particle.Play();
        }
        _feverSlider.DOValue(0, _feverTime)
            .OnComplete(() =>
            {
                _feverGenerator.SetActive(false);
                _fieldGenerator.SetActive(true);
                _feverBGM.Stop();
                _mainBGM.Play();
                var nextSpeed = _gameSpeedController.CurrentSpeed + 0.3f;
                if (nextSpeed <= 2.0)
                {
                    _gameSpeedController.ChangeSpeed(nextSpeed);
                }
                _feverEnabled = false;
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
            _scoreText.text = $"Score: {updateValue.ToString("00000")}"; // �i"f0" �� "0" �́A�����_�ȉ��̌����w��j
            yield return null;
        }
        while (Time.time < endTime);
        _scoreText.text = $"Score: {endScore.ToString("00000")}";
    }
}
