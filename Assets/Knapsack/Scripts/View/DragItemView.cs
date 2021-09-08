using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragItemView : MonoBehaviour
{
    [SerializeField] Text dragName;

    public void SetDragInfo(string name)
    {
        dragName.text = name;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetPosition(Vector2 uguiPos)
    {
        transform.localPosition = uguiPos;
    }
}
