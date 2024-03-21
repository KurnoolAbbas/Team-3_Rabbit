using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceeneLoader : MonoBehaviour
{
    public string sceneName; // Name of the scene to load (e.g., "NewScene")

    public void LoadSceneOnClick()
    {
        SceneManager.LoadScene(sceneName); // Load the specified scene
    }
}

