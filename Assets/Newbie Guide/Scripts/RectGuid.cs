using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RectGuid : MonoBehaviour
{
    [SerializeField]RectTransform _targetUI;
    [SerializeField] Material _rectMaterial;
    Vector3[] _targetCorner = new Vector3[4];
    Vector2 _center;
    Vector2 _widHei;

    public void SetGuid(Canvas canvas ,RectTransform target)
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
        _widHei.x = (_targetCorner[3].x - _targetCorner[0].x) * 0.5f;
        _widHei.y = (_targetCorner[1].y - _targetCorner[0].y) * 0.5f;
        _center.x = _targetCorner[0].x + _widHei.x;
        _center.y = _targetCorner[0].y + _widHei.y;

        _rectMaterial.SetVector("_Center", _center);
        _rectMaterial.SetFloat("_SliderX", _widHei.x);
        _rectMaterial.SetFloat("_SliderY", _widHei.y);

    }
}
    
