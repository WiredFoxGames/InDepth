using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadingScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Image pr;
    void Start()
    {
        
        StartCoroutine(LoadAsyncOperation());
        
    }

    IEnumerator LoadAsyncOperation()
    {
        AsyncOperation baseLevel = SceneManager.LoadSceneAsync("GameScene 1");
        
        while (baseLevel.progress < 1f)
        {
            yield return new WaitForEndOfFrame();
        }
        
    }
}
