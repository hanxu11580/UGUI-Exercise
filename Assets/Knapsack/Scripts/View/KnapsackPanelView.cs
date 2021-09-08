using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnapsackPanelView : MonoBehaviour
{
    GridView[] gridViews; 

    private void Start()
    {
        gridViews = new GridView[transform.childCount];
        for (int i = 0,count = transform.childCount; i < count; i++)
        {
            gridViews[i] = transform.GetChild(i).GetComponent<GridView>();
            gridViews[i].GridID = i;
        }
    }

    /// <summary>
    /// 获得一个空位
    /// </summary>
    public GridView GetEmptyGrid()
    {
        foreach (var grid in gridViews)
        {
            if(grid.transform.childCount == 0)
            {
                return grid;
            }
        }

        return null;
    }


}
