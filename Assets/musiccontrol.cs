using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton instance
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();
                if (_instance == null)
                {
                    GameObject AudioManagerObject = new GameObject("AudioManager");
                    _instance = AudioManagerObject.AddComponent<AudioManager>();
                    DontDestroyOnLoad(AudioManagerObject);
                }
            }
            return _instance;
        }
    }

    public GameObject soundON;
    public GameObject soundOFF;

    // Method to mute all audio sources
    public void MuteAllAudio()
    {
        AudioListener.volume = 0;
        if (soundOFF != null) soundOFF.SetActive(false);
        if (soundON != null) soundON.SetActive(true);
    }

    // Method to unmute all audio sources
    public void UnmuteAllAudio()
    {
        AudioListener.volume = 1;
        if (soundOFF != null) soundOFF.SetActive(true);
        if (soundON != null) soundON.SetActive(false);
    }
}
