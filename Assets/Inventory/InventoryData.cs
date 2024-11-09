using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Inventory Data")]
public class InventoryData : ScriptableObject
{
    // Holds reference to all the items and amounts
    private Dictionary<ItemData, int> inventoryDictionary = new Dictionary<ItemData, int>();

    public Dictionary<ItemData, int> InventoryDictionary { get { return inventoryDictionary; } }

    public void AddItem(ItemData _item)
    {
        if (inventoryDictionary.ContainsKey(_item)) inventoryDictionary[_item]++;
        else inventoryDictionary.Add(_item, 1);

        Debug.Log($"Item added: {_item.name}");
    }

    public void AddItems(ItemData _item, int _amount)
    {
        if (inventoryDictionary.ContainsKey(_item)) inventoryDictionary[_item] += _amount;
        else inventoryDictionary.Add(_item, _amount);
    }

    // Attempts to remove an item, returns false if there were none
    public bool RemoveItem(ItemData _item)
    {
        if (!inventoryDictionary.ContainsKey(_item)) return false;
        else inventoryDictionary[_item]--;

        if (inventoryDictionary[_item] <= 0) inventoryDictionary.Remove(_item);
        return true;
    }

    public void RemoveItems(ItemData _item, int _amount)
    {
        if (inventoryDictionary[_item] < _amount) return;
        else inventoryDictionary[_item] -= _amount;

        if (inventoryDictionary[_item] <= 0) inventoryDictionary.Remove(_item);
    }    
}