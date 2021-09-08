using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemModel
{
    static Dictionary<int, Item> _saveItems;

    static ItemModel()
    {
        _saveItems = new Dictionary<int, Item>();
    }


    public static void Add(int gridId, Item item)
    {
        if(_saveItems.TryGetValue(gridId, out Item old))
        {
            _saveItems[gridId] = item;
            return;
        }
        _saveItems.Add(gridId, item);
    }

    public static void Remove(int gridId)
    {
        if (_saveItems.ContainsKey(gridId))
        {
            _saveItems.Remove(gridId);
        }
    }

    public static Item GetItem(int gridId)
    {
        if (_saveItems.TryGetValue(gridId, out Item old))
        {
            return _saveItems[gridId];
        }
        return null;
    }
}
