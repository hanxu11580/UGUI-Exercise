using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipSystem : MonoBehaviour
{
    public static TipSystem Instance;

    [SerializeField] TipsTools tipsTools;

    private void Awake()
    {
        Instance = this;        
    }

    public static void ShowTips(string titleStr, string contentStr)
    {
        Instance.tipsTools.SetText(titleStr, contentStr);
        Instance.tipsTools.gameObject.SetActive(true);
    }

    public static void HideTips()
    {
        Instance.tipsTools.gameObject.SetActive(false);
    }
}
