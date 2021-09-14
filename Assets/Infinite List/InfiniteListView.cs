using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InfiniteListView : MonoBehaviour
{
    [Header("模拟数据量")] public int SimulationDataCount;

    [SerializeField] RectTransform _content;
    ScrollRect _scrollRect;
    RectTransform _selfRect;
    Vector3[] _windowCorner;
    #region 数据
    List<TaskModel> _taskModels;
    Vector2Int _windowEdge;
    #endregion
    //Task Item 任务格子信息
    LoopItem[] _loopItems;
    Vector3[] _itemCorner;
    LoopItem _firstItem;
    LoopItem _lastItem;
    //End

    private void Start()
    {
        _scrollRect = GetComponent<ScrollRect>();
        _selfRect = GetComponent<RectTransform>();
        _loopItems = new LoopItem[_content.childCount];
        LoadTaskData();
        for (int i = 0, count = _content.childCount; i < count; i++)
        {
            Transform t = _content.GetChild(i);
            _loopItems[i] = new LoopItem()
            {
                itemRectTran = t.GetComponent<RectTransform>(),
                itemTmpText = t.GetComponentInChildren<TextMeshProUGUI>()
            };
            // 初始化Task Item UI
            _loopItems[i].itemTmpText.text = _taskModels[i].textInfo;
        }

        _firstItem = _loopItems[0];
        _lastItem = _loopItems[_content.childCount - 1];
        //数据
        _windowEdge = Vector2Int.zero;
        _windowEdge.x = 0;
        _windowEdge.y = _content.childCount - 1;
        //
        _itemCorner = new Vector3[4];
        _windowCorner = new Vector3[4];
        _selfRect.GetWorldCorners(_windowCorner);
        LoopItem.ItemWidthHeight = new Vector2(_loopItems[0].itemRectTran.rect.width, _loopItems[0].itemRectTran.rect.height);

        // 设置大小为了让scrollbar归一为0~1
        _content.sizeDelta = new Vector2(_content.sizeDelta.x, _taskModels.Count * LoopItem.ItemWidthHeight.y);
    }

    private void Update()
    {
        if(_scrollRect.verticalNormalizedPosition > 1f)
        {
            _scrollRect.verticalNormalizedPosition = 1f;
        }
        if(_scrollRect.verticalNormalizedPosition < 0f)
        {
            _scrollRect.verticalNormalizedPosition = 0f;
        }


        _firstItem.GetWorldCorners(_itemCorner);
        if (_itemCorner[0].y > _windowCorner[1].y)
        {
            SetLastItem(_firstItem);
        }

        _lastItem.GetWorldCorners(_itemCorner);
        if(_itemCorner[1].y < _windowCorner[0].y)
        {
            SetFirstItem(_lastItem);
        }
    }

    void SetLastItem(LoopItem loopItem)
    {
        if (_windowEdge.y == _taskModels.Count - 1) return;
        loopItem.SetLastItem(ref _lastItem);

        int nextIdx = Array.IndexOf(_loopItems, loopItem) + 1;
        if (nextIdx == _loopItems.Length) nextIdx = 0;
        _firstItem = _loopItems[nextIdx];

        _windowEdge += Vector2Int.one;
        loopItem.itemTmpText.text = _taskModels[_windowEdge.y].textInfo;

    }

    void SetFirstItem(LoopItem loopItem)
    {
        if (_windowEdge.x == 0) return;
        loopItem.SetFirstItem(ref _firstItem);

        int nextIdx = Array.IndexOf(_loopItems, loopItem) - 1;
        if (nextIdx == -1) nextIdx = _loopItems.Length - 1;
        _lastItem = _loopItems[nextIdx];

        _windowEdge -= Vector2Int.one;
        loopItem.itemTmpText.text = _taskModels[_windowEdge.x].textInfo;

    }

    void LoadTaskData()
    {
        _taskModels = new List<TaskModel>();
        for (int i = 1; i <= SimulationDataCount; i++)
        {
            _taskModels.Add(new TaskModel($"Task - {i}"));
        }
    }
}

public class TaskModel
{
    public string textInfo;

    public TaskModel(string text)
    {
        textInfo = text;
    }    
}

public class LoopItem
{
    public RectTransform itemRectTran;
    public TextMeshProUGUI itemTmpText;

    public static  Vector3 ItemWidthHeight;

    public void GetWorldCorners(Vector3[] corners)
    {
        itemRectTran.GetWorldCorners(corners);
    }

    public void SetFirstItem(ref LoopItem firstItem)
    {
        itemRectTran.transform.SetAsFirstSibling();
        itemRectTran.localPosition = firstItem.itemRectTran.localPosition + Vector3.up * ItemWidthHeight.y;
        firstItem = this;
    }

    public void SetLastItem(ref LoopItem lastItem)
    {
        itemRectTran.transform.SetAsLastSibling();
        itemRectTran.localPosition = lastItem.itemRectTran.localPosition - Vector3.up * ItemWidthHeight.y;
        lastItem = this;
    }
}
