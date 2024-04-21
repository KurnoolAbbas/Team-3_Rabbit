using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton instance
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            // If the instance doesn't exist yet, try to find it in the scene
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();

                // If no instance is found, create a new GameObject and attach AudioManager to it
                if (_instance == null)
                {
                    GameObject audioManagerGameObject = new GameObject("AudioManager");
                    _instance = audioManagerGameObject.AddComponent<AudioManager>();
                }
            }
            return _instance;
        }
    }

    // Sound effects toggle
    private bool soundEffectsEnabled = true;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Ensure only one instance of AudioManager exists
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;

        // Persist across scenes
        DontDestroyOnLoad(this.gameObject);
    }

    // Method to toggle sound effects
    public void ToggleSoundEffects(bool isEnabled)
    {
        soundEffectsEnabled = isEnabled;
        AudioListener.volume = isEnabled ? 1 : 0;

        // Optionally, you can mute or unmute all sound effects here
        // Example: AudioListener.volume = isEnabled ? 1f : 0f;
    }

    // Method to check if sound effects are enabled
    public bool AreSoundEffectsEnabled()
    {
        return soundEffectsEnabled;
    }
}
