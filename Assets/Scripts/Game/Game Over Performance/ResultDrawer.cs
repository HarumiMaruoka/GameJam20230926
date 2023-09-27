// 日本語対応
using System;
using UnityEngine;
using DG.Tweening;

public class ResultDrawer : MonoBehaviour
{
    [SerializeField]
    private RectTransform _resultUI;
    [SerializeField]
    private Ease _ease;
    [SerializeField]
    private float _duration;
    [SerializeField]
    private Vector2 _endPos;

    // リザルト画面を表示する。
    public void Play(Action onComplete)
    {
        _resultUI.gameObject.SetActive(true);
        _resultUI.DOAnchorPos(_endPos, _duration).
            SetEase(_ease).
            OnComplete(() => onComplete?.Invoke());
    }
}