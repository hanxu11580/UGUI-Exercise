using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PageScrollView : MonoBehaviour, IEndDragHandler
{
    ScrollRect _scrollRect;
    // 总共有几页
    int _pageCount;
    // 根据滑动进度，进行比较，然后设置到显示完全页面的进度
    [SerializeField]float[] _signProgress;
    [SerializeField]float[] _targetProgress;

    bool _startScroll;
    float _targetVal;

    public float speed;
    public bool IsStartAutoScroll; //自动滚动

    private void Start()
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

    private void Update()
    {
        if (_startScroll)
        {
            var value = Mathf.Lerp(_scrollRect.horizontalNormalizedPosition, _targetVal, Time.deltaTime * speed);
            if(Mathf.Abs(value-_targetVal) < 0.0001f)
            {
                _startScroll = false;
            }
            _scrollRect.horizontalNormalizedPosition = value;
        }

        if (IsStartAutoScroll)
        {
            if (_targetProgress.GetEnumerator().MoveNext())
            {
                Debug.Log(_targetProgress.GetEnumerator().Current);
            }
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
                _targetVal = _targetProgress[i];
                _startScroll = true;
                break;
            }
        }
    }
}
