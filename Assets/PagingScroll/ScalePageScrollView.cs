using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 滑动Scale会变化
/// </summary>
public class ScalePageScrollView : PageScrollView
{
    Transform[] _pages;
    ScrollViewport _scrollViewport;

    protected override void Start()
    {
        base.Start();
        var content = transform.Find("Viewport/Content");
        _pages = new Transform[content.childCount];
        for (int i = 0,cCount = content.childCount; i < cCount; i++)
        {
            _pages[i] = content.GetChild(i);
        }
        _scrollViewport = new ScrollViewport(_targetProgress, _pages);
    }


    protected override void Update()
    {
        base.Update();
        _scrollViewport.Update(_scrollRect.horizontalNormalizedPosition);
    }
}

public class ScrollViewport
{
    float[] _targets;
    Transform[] _pages;

    readonly float FixedInterval;

    public ScrollViewport(float[] targets, Transform[] trans)
    {
        _targets = targets;
        _pages = trans;
        FixedInterval = _targets[1];
    }

    public void Update(float val)
    {
        for (int i = 0, count = _pages.Length; i < count; i++)
        {
            Transform trans = _pages[i];
            float progress = _targets[i];
            float res = Mathf.Abs(progress - val) / FixedInterval;
            res = Mathf.Clamp(res, 0f, 1f);
            trans.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 0.6f, res);
        }
    }

}
