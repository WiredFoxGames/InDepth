using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUi;
    public GameObject saveSure;
    public GameObject popupUI;
    private float popupTime = 1.2f;
    public float timer = 0.0f;

    [SerializeField] private FPcontroller player;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
                
            }
            else
            {
                  
                Pause();
            }
        }
        
        
        //Timer for popup
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            HidePopup();
        }
    }

    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        saveSure.SetActive(false);
    }

    void Pause()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
       
    }

    public void dontSave()
    {
        saveSure.SetActive(false);
    }

    public void saveCorrect()
    {
        player.SaveGame();
        ShowPopup();
        saveSure.SetActive(false);
        player.notResource.SetText("Game saved!");
        
    }
    public void SaveGame()
    {
        saveSure.SetActive(true);
       
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void ShowPopup()
    {
        popupUI.SetActive(true);
        timer = popupTime;
    }

    public void HidePopup()
    {
        popupUI.SetActive(false);
    }

    
}
