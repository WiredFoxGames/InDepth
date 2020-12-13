using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryChar : MonoBehaviour
{
    // Start is called before the first frame update
    private bool _inventoryEnabled;
    public GameObject inventory;
    public Camera cam;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _inventoryEnabled = !_inventoryEnabled;
        }
        
        if (_inventoryEnabled)
        {
            inventory.SetActive(true);
        }
        else
        {    
            inventory.SetActive(false);
        }
    }
 
}
