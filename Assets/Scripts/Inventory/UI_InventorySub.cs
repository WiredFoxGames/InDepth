using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class UI_InventorySub : MonoBehaviour
{
    
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private FPcontroller player;
    private Submarine submarine;
    public bool isFull;
    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    }
    
    public void SetSubmarine(Submarine submarine)
    {
        this.submarine = submarine;
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
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 82f;
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRecTransform =
                Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRecTransform.gameObject.SetActive(true);
            itemSlotRecTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                //usar item
            };
            itemSlotRecTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                isFull = false;
                Item duplicateItem = new Item {itemType = item.itemType, amount = item.amount};
                inventory.RemoveItem(item);
                ItemWorld.DropItem(submarine.GetPosition(), duplicateItem);

            };
            
            itemSlotRecTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRecTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            TextMeshProUGUI amountText = itemSlotRecTransform.Find("amount").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                amountText.SetText(item.amount.ToString());
            }
            else
            {
                amountText.SetText("");
            }
            
            x++;
            if (x > 8)
            {
                x = 0;
                y--;
                if (y < -2)
                {
                    isFull = true;
                }
            }

            
        }
    }
}
