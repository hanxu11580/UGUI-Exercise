using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item
{
    /// <summary>
    /// 防御
    /// </summary>
    public int Defend { get; private set; }

    public Armor(int id, string name, string desc, int buyPrice, int sellPrice, int defend) : base(id, name, desc, buyPrice, sellPrice)
    {
        Defend = defend;
    }
}
