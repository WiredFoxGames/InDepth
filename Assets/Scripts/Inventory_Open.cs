using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Open : MonoBehaviour
{
    // Start is called before the first frame update
    private bool _inventoryEnabled;
    public GameObject inventory;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _inventoryEnabled = true;
        }

        if (_inventoryEnabled)
        {
            inventory.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _inventoryEnabled = false;
        }

        if (_inventoryEnabled == false)
        {
            inventory.SetActive(false);
        }
    }

}