
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CircleGuid : MonoBehaviour
{
    [SerializeField] RectTransform _targetUI;
    [SerializeField] Material _rectMaterial;
    Vector3[] _targetCorner = new Vector3[4];
    Vector2 _center;
    float _radius;

    public void SetGuid(Canvas canvas, RectTransform target)
    {
        _targetUI = target;
        _targetUI.GetWorldCorners(_targetCorner);
        // 转成屏幕坐标、然后转成UGUI坐标，这个shader是以屏幕中心点为原点的
        for (int i = 0; i < 4; i++)
        {
            var screenI = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, _targetCorner[i]);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), screenI, canvas.worldCamera, out Vector2 uguiPos);
            _targetCorner[i] = uguiPos;
        }
        var wid = (_targetCorner[3].x - _targetCorner[0].x) * 0.5f;
        var hei = (_targetCorner[1].y - _targetCorner[0].y) * 0.5f;
        _center.x = _targetCorner[0].x + wid;
        _center.y = _targetCorner[0].y + hei;
        _radius = wid >= hei ? wid : hei;

        _rectMaterial.SetVector("_Center", _center);
        _rectMaterial.SetFloat("_Slider", _radius);
    }


    private void Update()
    {
        SetGuid(GameObject.Find("UICanvas").GetComponent<Canvas>(), _targetUI);
    }
}


