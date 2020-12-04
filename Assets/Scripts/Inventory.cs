using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{

    public event EventHandler OnItemListChanged;
    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();
        /*AddItem(new Item {itemType = Item.ItemType.Iron, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Crystal, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Meat, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Pearl, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Health, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Rock, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Iron, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Crystal, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Meat, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Pearl, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.H2O, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Health, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Rock, amount = 1});*/
        
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}