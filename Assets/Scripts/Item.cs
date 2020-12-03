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
      Canicadora,
      LaserBeam,
      DoubleCannon
   }

   public ItemType itemType;
   public int amount;
   
}
