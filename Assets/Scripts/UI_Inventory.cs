using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }
    
    private void RefreshInventoryItems()
    {
        
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 82f;
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRecTransform =
                Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRecTransform.gameObject.SetActive(true);
            itemSlotRecTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRecTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            x++;
            if (x > 8)
            {
                x = 0;
                y--;
            }
        }
    }
}