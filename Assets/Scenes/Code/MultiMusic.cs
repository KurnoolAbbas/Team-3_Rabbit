using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiMusic : MonoBehaviour
{
    private static MultiMusic multiMusic;

    // Reference to the audio source
    private AudioSource audioSource;

    void Awake()
    {
        if (multiMusic == null)
        {
            multiMusic = this;
            DontDestroyOnLoad(multiMusic);
        }
        else
        {
            Destroy(gameObject);
        }

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoadedHandler;
        SceneManager.sceneUnloaded += OnSceneUnloadedHandler;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoadedHandler;
        SceneManager.sceneUnloaded -= OnSceneUnloadedHandler;
    }

    void OnSceneLoadedHandler(Scene scene, LoadSceneMode mode)
    {
        // Check if the loaded scene is the multiplication screen
        if (scene.name == "multScreen")
        {
            // Start playing the background music
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    void OnSceneUnloadedHandler(Scene scene)
    {
        // Check if the unloaded scene is the multiplication screen
        if (scene.name == "multScreen")
        {
            // Stop the background music
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
