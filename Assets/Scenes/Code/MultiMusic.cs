using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiMusic : MonoBehaviour
{
    private static MultiMusic multiMusic;
    void Awake()
    {
        if(multiMusic == null)
        {
            multiMusic = this;
            DontDestroyOnLoad(multiMusic);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SceneManager.sceneUnloaded += OnSceneUnloadedHandler;
    }

    private void OnSceneUnloaded(Scene arg0)
    {
        throw new NotImplementedException();
    }

    void OnDestroy()
    {
         SceneManager.sceneUnloaded -= OnSceneUnloadedHandler;
    }

    void OnSceneUnloadedHandler(Scene scene)
    {
        // Check if the unloaded scene is the multiplication scene
        if (scene.name == "multScreen")
        {
            // Stop the background music
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }


}
