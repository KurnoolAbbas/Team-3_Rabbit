using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceeneLoader : MonoBehaviour
{
    public string sceneName; // Name of the scene to load (e.g., "NewScene")

    public void LoadSceneOnClick()
    {
        mulLogic gameLogic = GameObject.FindObjectOfType<mulLogic>();
        if (sceneName == "multScreen")
        {
            
            if (gameLogic != null)
            {
                gameLogic.SaveGameState(); // Save the game state before loading a new scene
            }
        }
        if(sceneName == "TableSceneSingle")
        {
            if (gameLogic != null)
            {
                gameLogic.LoadGameState();
            }
        }
        SceneManager.LoadScene(sceneName); // Load the specified scene
    }
}

