using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();
        AddItem(new Item {itemType = Item.ItemType.Iron, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Crystal, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Rock, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Rock, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Rock, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Rock, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Rock, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Rock, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Rock, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Rock, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Rock, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Rock, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Rock, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Rock, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Rock, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Rock, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Rock, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Rock, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Rock, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Rock, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Rock, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Rock, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Rock, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Rock, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Rock, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Rock, amount = 1});
        AddItem(new Item {itemType = Item.ItemType.Rock, amount = 1});
        
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}