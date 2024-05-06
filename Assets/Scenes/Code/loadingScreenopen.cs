using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadingScreenopen : MonoBehaviour
{
    // Start is called before the first frame update
    public string nextSceneName; // Set the name of the scene to load
    public float delayTime = 1.9999f; // Time to wait before loading (in seconds)



    void Start()
    {
        StartCoroutine(LoadSceneAfterDelay());
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene("loading");
    }
}