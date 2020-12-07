using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
   public enum ItemType
   {
      Rock,
      Iron,
      Crystal,
      Pearl,
      Meat,
      H2O,
      Health,
      LaCanicadora,
      ElSuspenso,
      DoubleCannon
   }

   public ItemType itemType;
   public int amount;

   public Sprite GetSprite()
   {
      switch (itemType)
      {
            default:
            case ItemType.Rock: return ItemAssets.Instance.rockSprite;
            case ItemType.Iron: return ItemAssets.Instance.ironSprite;
            case ItemType.Crystal: return ItemAssets.Instance.crystalSprite;
            case ItemType.Pearl: return ItemAssets.Instance.pearlSprite;
            case ItemType.Meat: return ItemAssets.Instance.meatSprite;
            case ItemType.H2O: return ItemAssets.Instance.h2OSprite;
            case ItemType.Health: return ItemAssets.Instance.healthSprite;
            
      }
   }
}
