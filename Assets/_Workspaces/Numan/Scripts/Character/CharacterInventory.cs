using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    public List<Item> items;

    public void DisplayItem(string _name)
    {
        foreach (var _item in items)
        {
            if (_item.Name == _name)
            {
                _item.heldItem.SetActive(true);
            }
            else
            {
                _item.heldItem.SetActive(false);
            }
        }
    }

    public void AddItem(string _name, int _count)
    {
        foreach (var _item in items)
        {
            if (_item.Name == _name)
            {
                _item.Count += _count;
                DisplayItem(_item.Name);
                break;
            }
        }
    }
}
[System.Serializable]
public class Item
{
    public string Name;
    public int Count;
    public GameObject heldItem;
}
