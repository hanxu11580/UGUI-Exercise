using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public int Id { get; private set; }

    public string Name { get; private set; }

    public string Desc { get; private set; }

    public int BuyPrice { get; private set; }

    public int SellPrice { get; private set; }

    public Item(int id, string name, string desc, int buyPrice, int sellPrice)
    {
        Id = id;
        Name = name;
        Desc = desc;
        BuyPrice = buyPrice;
        SellPrice = sellPrice;
    }
}
