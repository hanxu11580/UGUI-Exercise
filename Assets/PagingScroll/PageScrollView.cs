using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PageScrollView : MonoBehaviour, IEndDragHandler,IBeginDragHandler
{
    protected ScrollRect _scrollRect;
    // 总共有几页
    int _pageCount;
    // 根据滑动进度，进行比较，然后设置到显示完全页面的进度
    [SerializeField]float[] _signProgress;
    [SerializeField] protected float[] _targetProgress;

    bool _startScroll;
    float _targetVal;
    protected int _targetIdx;

    public float speed;

    protected virtual void Start()
    {
        _scrollRect = GetComponent<ScrollRect>();
        _pageCount = transform.Find("Viewport/Content").childCount;
        _signProgress = new float[_pageCount];
        _targetProgress = new float[_pageCount];
        for (int i = 0; i < _pageCount; i++)
        {
            _targetProgress[i] = (1f / (_pageCount - 1)) * i;
            _signProgress[i] = _targetProgress[i] + ((1f / (_pageCount - 1))) * 0.5f;
        }
    }

    protected virtual void Update()
    {
        if (_startScroll)
        {
            var value = Mathf.Lerp(_scrollRect.horizontalNormalizedPosition, _targetVal, Time.deltaTime * speed);
            if(Mathf.Abs(value - _targetVal) < 0.0001f)
            {
                value = _targetVal;
                _startScroll = false;
            }
            _scrollRect.horizontalNormalizedPosition = value;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var value = _scrollRect.horizontalNormalizedPosition;
        for (int i = 0; i < _pageCount; i++)
        {
            var signProgress = _signProgress[i];
            if (value <= signProgress)
            {
                _targetIdx = i;
                _targetVal = _targetProgress[i];
                _startScroll = true;
                break;
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startScroll = false;
    }
}
