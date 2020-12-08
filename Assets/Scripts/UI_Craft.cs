using System;
using CodeMonkey.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Craft : MonoBehaviour
{
    private Transform container;
    private Transform craftItemTemplate;
    private ICrafter craftCustomer;
    private void Awake()
    {
        container = transform.Find("container");
        craftItemTemplate = container.Find("craftItemTemplate");
        craftItemTemplate.gameObject.SetActive(true);
    }


    private void Start()
    {
        
        CreateItemButton(Item.ItemType.ElSuspenso,Item.GetSpriteCr(Item.ItemType.ElSuspenso), "ElSuspenso", 
            Item.GetCostCrystal(Item.ItemType.ElSuspenso), Item.GetCostRock(Item.ItemType.ElSuspenso), Item.GetCostPearl(Item.ItemType.ElSuspenso),
            Item.GetCostIron(Item.ItemType.ElSuspenso), Item.GetCostMeat(Item.ItemType.ElSuspenso), 0);
        CreateItemButton(Item.ItemType.LaCanicadora,Item.GetSpriteCr(Item.ItemType.LaCanicadora), "LaCanicadora", 
            Item.GetCostCrystal(Item.ItemType.LaCanicadora), Item.GetCostRock(Item.ItemType.LaCanicadora), Item.GetCostPearl(Item.ItemType.LaCanicadora),
            Item.GetCostIron(Item.ItemType.LaCanicadora), Item.GetCostMeat(Item.ItemType.LaCanicadora), 1);
        CreateItemButton(Item.ItemType.DoubleCannon,Item.GetSpriteCr(Item.ItemType.DoubleCannon), "DoubleCannon", 
            Item.GetCostCrystal(Item.ItemType.DoubleCannon), Item.GetCostRock(Item.ItemType.DoubleCannon), Item.GetCostPearl(Item.ItemType.DoubleCannon),
            Item.GetCostIron(Item.ItemType.DoubleCannon), Item.GetCostMeat(Item.ItemType.DoubleCannon), 2);
        CreateItemButton(Item.ItemType.Health,Item.GetSpriteCr(Item.ItemType.Health), "Health", 
            Item.GetCostCrystal(Item.ItemType.Health), Item.GetCostRock(Item.ItemType.Health), Item.GetCostPearl(Item.ItemType.Health),
            Item.GetCostIron(Item.ItemType.Health), Item.GetCostMeat(Item.ItemType.Health), 3);
        craftItemTemplate.gameObject.SetActive(false);
        Hide();
    }

    private void CreateItemButton(Item.ItemType itemType, Sprite itemSprite, String itemName, int costCrystal,
        int costRock, int costPearl, int costIron, int costMeat, int positionIndex)
    {
        Transform craftItemTransform = Instantiate(craftItemTemplate, container);
        RectTransform craftRectTransform = craftItemTransform.GetComponent<RectTransform>();

        float craftItemHeight = 140f;
        craftRectTransform.anchoredPosition = new Vector2(-14, -craftItemHeight * positionIndex);
        craftItemTransform.Find("name").GetComponent<TextMeshProUGUI>().SetText(itemName);
        craftItemTransform.Find("priceOne").GetComponent<TextMeshProUGUI>().SetText(costCrystal.ToString());
        craftItemTransform.Find("priceTwo").GetComponent<TextMeshProUGUI>().SetText(costIron.ToString());
        craftItemTransform.Find("priceThree").GetComponent<TextMeshProUGUI>().SetText(costPearl.ToString());
        craftItemTransform.Find("priceFour").GetComponent<TextMeshProUGUI>().SetText(costRock.ToString());
        craftItemTransform.Find("priceFive").GetComponent<TextMeshProUGUI>().SetText(costMeat.ToString());
        craftItemTransform.Find("item").GetComponent<Image>().sprite = itemSprite;

        craftItemTransform.GetComponent<Button_UI>().ClickFunc = () =>
        {
            TryCraftItem(itemType);
        };
    }

    private void TryCraftItem(Item.ItemType itemType)
    {
        craftCustomer.CraftItem(itemType);
    }

    public void Show(ICrafter craftCustomer)
    {
        this.craftCustomer = craftCustomer;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
