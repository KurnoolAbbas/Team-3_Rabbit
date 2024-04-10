using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public AudioClip soundClip;

    private Button button;
    private AudioSource audioSource;

    void Start()
    {
        button = GetComponent<Button>();
        audioSource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
        button.onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
        audioSource.PlayOneShot(soundClip);
    }
}
