using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class GridView : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler, IBeginDragHandler, IDragHandler,IEndDragHandler
{
    public int GridID { get; set; }

    public static Action<int> OnEnter;
    public static Action OnExit;
    //拖拽
    public static Action<GridView> OnLeftBeginDrag;
    public static Action<GridView, GridView> OnLeftEndDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftBeginDrag?.Invoke(this);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GridView targetGrid = null;
            if (eventData.pointerEnter != null)
            {
                targetGrid = eventData.pointerEnter.GetComponent<GridView>();
            }
            OnLeftEndDrag?.Invoke(this, targetGrid);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter.CompareTag("GridTag"))
        {
            OnEnter?.Invoke(GridID);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter.CompareTag("GridTag"))
        {
            OnExit?.Invoke();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
