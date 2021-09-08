using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public int Damage { get; private set; }

    public Weapon(int id, string name, string desc, int buyPrice, int sellPrice, int damage):base(id,name,desc,buyPrice,sellPrice)
    {
        Damage = damage;
    }
}
