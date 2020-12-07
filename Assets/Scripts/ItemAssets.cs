using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public Transform pfItemWorld;
    public Sprite rockSprite;
    public Sprite ironSprite;
    public Sprite crystalSprite;
    public Sprite pearlSprite;
    public Sprite meatSprite;
    public Sprite h2OSprite;
    public Sprite healthSprite;
    public Sprite laCanicadoraSprite;
    public Sprite elSuspensoSprite;
    public Sprite doubleCannonSprite;
    public static ItemAssets Instance { get; private set;}

    private void Awake()
    {
        Instance = this;
    }
    
 
    
}
