using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;


public class NewBehaviourScript : Image

{
    public string nextSceneName; // Set the name of the scene to load
    public float delayTime = 3.0f; // Time to wait before loading (in seconds)
    public override Material materialForRendering
    {
        get
        {
            Material material = new Material(base.materialForRendering);
            material.SetInt("_StencilComp", (int)CompareFunction.NotEqual);
            return material;
        }
    }
    

    void Start()
    {
        StartCoroutine(LoadSceneAfterDelay());
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayTime); Debug.Log("mama");
        SceneManager.LoadScene("loading");
    }

}

