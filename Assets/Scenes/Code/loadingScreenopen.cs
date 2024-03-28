using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadingScreenopen : MonoBehaviour
{
    // Start is called before the first frame update
    public string nextSceneName; // Set the name of the scene to load
    public float delayTime = 3.0f; // Time to wait before loading (in seconds)
    


    void Start()
    {
        StartCoroutine(LoadSceneAfterDelay());
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayTime); 
        //SceneManager.LoadScene("loading");
        
        EditorSceneManager.OpenScene(nextSceneName);
    }
}
