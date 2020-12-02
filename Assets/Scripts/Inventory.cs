using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    private bool _inventoryEnabled;
    public GameObject inventory;
    private int _allSlots;
    private int _enabledSlots;
    private GameObject[] _slot;
    public GameObject slotHolder;

    void Start()
    {
        _allSlots = slotHolder.transform.childCount;
        _slot = new GameObject[_allSlots];
        for (int i = 0; i < _allSlots; i++)
        {
            _slot[i] = slotHolder.transform.GetChild(i).gameObject;
        }
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