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
            case ItemType.LaCanicadora: return ItemAssets.Instance.laCanicadoraSprite;
            case ItemType.ElSuspenso: return ItemAssets.Instance.elSuspensoSprite;
            case ItemType.DoubleCannon: return ItemAssets.Instance.doubleCannonSprite;
      }
   }

   public static Sprite GetSpriteCr(ItemType itemType)
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
         case ItemType.LaCanicadora: return ItemAssets.Instance.laCanicadoraSprite;
         case ItemType.ElSuspenso: return ItemAssets.Instance.elSuspensoSprite;
         case ItemType.DoubleCannon: return ItemAssets.Instance.doubleCannonSprite;
      }
   }

   
   public static int GetCostRock(ItemType itemType)
   {
      switch (itemType)
      {
         default:
            case ItemType.DoubleCannon: return 20;
            case ItemType.LaCanicadora: return 30;
            case ItemType.Health: return 0;
         case ItemType.ElSuspenso: return 40;
       }
   }
   
   public static int GetCostCrystal(ItemType itemType)
   {
      switch (itemType)
      {
         default:
         case ItemType.DoubleCannon: return 5;
         case ItemType.LaCanicadora: return 10;
         case ItemType.Health: return 5;
         case ItemType.ElSuspenso: return 30;
      }
   }

   
   public static int GetCostIron(ItemType itemType)
   {
      switch (itemType)
      {
         default:
         case ItemType.DoubleCannon: return 10;
         case ItemType.LaCanicadora: return 15;
         case ItemType.Health: return 5;
         case ItemType.ElSuspenso: return 30;
      }
   }
   
   public static int GetCostPearl(ItemType itemType)
   {
      switch (itemType)
      {
         default:
         case ItemType.DoubleCannon: return 0;
         case ItemType.LaCanicadora: return 1;
         case ItemType.Health: return 0;
         case ItemType.ElSuspenso: return 0;
      }
   }
   
   public static int GetCostMeat(ItemType itemType)
   {
      switch (itemType)
      {
         default:
         case ItemType.DoubleCannon: return 0;
         case ItemType.LaCanicadora: return 0;
         case ItemType.Health: return 15;
         case ItemType.ElSuspenso: return 0;
      }
   }
   
   
   public bool isStackable()
   {
      switch (itemType)
      {
         default:
            case ItemType.Crystal:
            case ItemType.Iron:
            case ItemType.Pearl:
            case ItemType.Meat:
            case ItemType.H2O:
               return true;
            case ItemType.LaCanicadora:
            case ItemType.DoubleCannon:
            case ItemType.ElSuspenso:
               return false;
      }
   }
}
