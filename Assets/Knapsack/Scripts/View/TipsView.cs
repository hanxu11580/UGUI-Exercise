using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsView : MonoBehaviour
{
    [SerializeField] Text textTips; //用于调整大小
    [SerializeField] Text textContent; //真正显示的东西

    static Vector2 DownRightConerOffset = new Vector2(20, -20);

    public void UpdateTipsContent(string text)
    {
        textTips.text = text;
        textContent.text = text;
    }

    public void ShowOrHideTips(Vector2 v2,bool isShow)
    {
        SetTipsPosition(v2);
        gameObject.SetActive(isShow);
    }

    void SetTipsPosition(Vector2 uguiPos)
    {
        // 向右下角偏移20个单位
        uguiPos += DownRightConerOffset;
        transform.localPosition = uguiPos;
    }

}
