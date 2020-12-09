using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Diagnostics;

public class ItemWorld : MonoBehaviour
{
    private Item item;
    private SpriteRenderer spriteRenderer;
    private Inventory inventory;


    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);
        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);
        return itemWorld;
    }


    public static ItemWorld DropItem(Vector3 dropPosition, Item item)
    {
        
        Vector3 randomDir = new Vector3(-2,0.2f,-1 );
        ItemWorld itemWorld = SpawnItemWorld(dropPosition + randomDir, item);
        itemWorld.GetComponent<Rigidbody>().AddForce(randomDir * 1f, ForceMode.Impulse);
        return itemWorld;
    }
    
    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

   

    public void SetItem(Item item)
    {
        this.item = item;
        
    }

   
}