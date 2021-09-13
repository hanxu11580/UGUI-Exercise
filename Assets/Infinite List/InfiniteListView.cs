using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfiniteListView : MonoBehaviour
{
    [SerializeField] Transform _content;
    ScrollRect _scrollRect;

    List<TaskModel> _taskModels;
    //Task Item
    RectTransform[] _itemRectTrans;
    TextMeshProUGUI[] _itemTmpTexts;
    //End
    private void Start()
    {
        _scrollRect = GetComponent<ScrollRect>();
        _itemRectTrans = new RectTransform[_content.childCount];
        _itemTmpTexts = new TextMeshProUGUI[_content.childCount];
        LoadTaskData();
        for (int i = 0, count = _content.childCount; i < count; i++)
        {
            Transform t = _content.GetChild(i);
            _itemRectTrans[i] = t.GetComponent<RectTransform>();
            Debug.Log(t.GetChild(0).name);
            _itemTmpTexts[i] = t.GetComponentInChildren<TextMeshProUGUI>();
            // 初始化Task Item UI
            _itemTmpTexts[i].text = _taskModels[i].textInfo;
        }
    }

    private void Update()
    {
        
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
