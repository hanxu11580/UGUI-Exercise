using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TipsTools : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _title;
    [SerializeField] TextMeshProUGUI _content;
    [SerializeField] LayoutElement _layoutElement;

    public int charLimit;

    RectTransform _rect;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }

    public void SetText(string titleStr, string contentStr)
    {
        _title.text = titleStr;
        _content.text = contentStr;
        SetLayoutElementEnable();
    }

    void SetLayoutElementEnable()
    {
        int titleLenght = _title.text.Length;
        int contentlenght = _content.text.Length;

        _layoutElement.enabled = (contentlenght > charLimit || titleLenght > charLimit) ? true : false;
    }

    private void Update()
    {
#if UNITY_EDITOR
        SetLayoutElementEnable();
#endif

        Vector3 mousePos = Input.mousePosition;
        transform.position = mousePos;
        // 根据鼠标位置动态调整 
        var p_x = mousePos.x / Screen.width;
        var p_y = mousePos.y / Screen.height;
        _rect.pivot = new Vector2(p_x, p_y);
        

    }
}
