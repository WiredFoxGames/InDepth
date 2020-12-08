using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICrafter
{
    void CraftItem(Item.ItemType itemType);
    //bool TryCraftItem(int crystalAmount, int ironAmount, int pearlAmount, int rockAmount, int meatAmount);
    bool TryCraftItem(int rockAmount);
}
