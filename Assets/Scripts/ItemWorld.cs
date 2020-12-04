using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    private Item item;
    private SpriteRenderer spriteRenderer;
    private Inventory inventory;
    public Rigidbody rg;
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);
        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);
        return itemWorld;
    }


    private void OnCollisionEnter(Collision other)
    {
        //ItemWorld itemWorld = other.collider.GetComponent<ItemWorld>();
        if (other.gameObject.tag == "Player")
        {
            Debug.Log(other);
            //Destroy(other.gameObject);
           
            /*inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();*/
        }
        
        
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetItem(Item item)
    {
        this.item = item;
        //spriteRenderer.sprite = item.GetSprite();
    }
}