
using UnityEngine;
using UnityEngine.SceneManagement;

//Class to make the scene manager, this will be usefull to manage our scenes 
//With only call functions in our scripts.
public class MySceneManager : MonoBehaviour
{
    //Variables.
    public static MySceneManager istance = null;
    
    //This is for instanciate the manager.
    private void Awake()
    {
        istance = this;
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
