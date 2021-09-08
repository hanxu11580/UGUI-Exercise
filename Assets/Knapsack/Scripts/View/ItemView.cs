using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] Text textName;


    public void UpdateItemName(string name)
    {
        textName.text = name;
    }
}
