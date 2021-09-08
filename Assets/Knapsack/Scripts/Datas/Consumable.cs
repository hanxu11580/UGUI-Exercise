using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item
{
    public int BackHp { get; private set; }

    public int BackMp { get; private set; }

    public Consumable(int id, string name, string desc, int buyPrice, int sellPrice, int backHp, int backMp) : base(id, name, desc, buyPrice, sellPrice)
    {
        BackHp = backHp;
        BackMp = backMp;
    }
}
