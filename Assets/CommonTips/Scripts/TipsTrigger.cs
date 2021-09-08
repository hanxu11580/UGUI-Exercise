using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TipsTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] string _title;
    [SerializeField] string _content;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TipSystem.ShowTips(_title, _content);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TipSystem.HideTips();
    }
}
