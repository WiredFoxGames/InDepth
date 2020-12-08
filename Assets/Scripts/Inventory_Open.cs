using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Open : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private UI_Craft uiCraft;
    
    private void OnTriggerEnter(Collider other)
    {
        ICrafter craftCustomer = other.GetComponent<ICrafter>();
        if (craftCustomer != null)
        {
            uiCraft.Show(craftCustomer);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ICrafter craftCustomer = other.GetComponent<ICrafter>();
        if (craftCustomer != null)
        {
            uiCraft.Hide();
        }
    }
}