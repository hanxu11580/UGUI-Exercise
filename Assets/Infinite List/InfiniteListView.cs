using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfiniteListView : MonoBehaviour
{
    [SerializeField] Transform _content;
    ScrollRect _scrollRect;
    RectTransform _selfRect;
    Vector3[] _windowCorner;

    List<TaskModel> _taskModels;
    //Task Item
    RectTransform[] _itemRectTrans;
    TextMeshProUGUI[] _itemTmpTexts;
    Vector3[] _itemCorner;
    //End
    private void Start()
    {
        _scrollRect = GetComponent<ScrollRect>();
        _selfRect = GetComponent<RectTransform>();
        _itemRectTrans = new RectTransform[_content.childCount];
        _itemTmpTexts = new TextMeshProUGUI[_content.childCount];
        LoadTaskData();
        for (int i = 0, count = _content.childCount; i < count; i++)
        {
            Transform t = _content.GetChild(i);
            _itemRectTrans[i] = t.GetComponent<RectTransform>();
            _itemTmpTexts[i] = t.GetComponentInChildren<TextMeshProUGUI>();
            // 初始化Task Item UI
            _itemTmpTexts[i].text = _taskModels[i].textInfo;
        }
        _itemCorner = new Vector3[4];
        _windowCorner = new Vector3[4];
        _selfRect.GetWorldCorners(_windowCorner);
    }

    private void Update()
    {
        foreach (var item in _itemRectTrans)
        {
            item.GetWorldCorners(_itemCorner);
            // 框上面的
            if(_itemCorner[0].y > _windowCorner[1].y)
            {
                item.gameObject.SetActive(false);
            }
        }
    }


    void LoadTaskData()
    {
        _taskModels = new List<TaskModel>();
        for (int i = 0; i < 100; i++)
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
