using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager istance = null;

    private void Awake()
    {
        istance = this;
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //This method load the scene of the base. 
    public void Base()
    {
        SceneManager.LoadScene("BaseScene");
    }
    
    //This method load the game scene.
    public void Game()
    {
        SceneManager.LoadScene("GameScene");
    }
}
