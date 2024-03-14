using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public Slider progressBar;

    public float countdownDuration = 3f;
    private float countdownTimer;
    private bool loadingComplete = false;

    private void Start()
    {
        countdownTimer = countdownDuration;

        // Check if UI Text and Slider components are assigned
        if (countdownText == null)
        {
            Debug.LogError("UI Text component not assigned to LoadingScreen script. Please assign it in the Inspector.");
        }

        if (progressBar == null)
        {
            Debug.LogError("Slider component not assigned to LoadingScreen script. Please assign it in the Inspector.");
        }

        // Start the loading process
        StartCoroutine(LoadingSequence());
    }

    private IEnumerator LoadingSequence()
    {
        float progress = 0f;

        while (progress < 1f)
        {
            // Update the countdown timer
            countdownTimer -= Time.deltaTime;

            // Display the countdown on the loading screen
            int roundedCountdown = Mathf.CeilToInt(countdownTimer);
            if (countdownText != null)
            {
                countdownText.text = "" + roundedCountdown;

                // Change the color of countdown text every second
                countdownText.color = GetCountdownColor(roundedCountdown);
            }

            // Update the progress bar
            progress = 1f - (countdownTimer / countdownDuration);
            if (progressBar != null)
            {
                progressBar.value = progress;
            }

            yield return null;
        }

        // Ensure the progress bar is at 100% when the countdown completes
        if (progressBar != null)
        {
            progressBar.value = 4f;
        }

        // Mark loading as complete
        loadingComplete = true;
    }

    private Color GetCountdownColor(int seconds)
    {
        // Define your color logic here
        if (seconds == 1)
        {
            return Color.green;
        }
        else if(seconds == 2)
        {
            return Color.yellow;
        }
        else if(seconds == 3)
        {
            return Color.blue;
        }
        else
        {
            return Color.red;
        }
    }

    private void Update()
    {
        // Check if the loading is complete, then load the main scene
        if (loadingComplete)
        {
            LoadMainScene();
        }
    }

    private void LoadMainScene()
    {
        // Load the main scene (replace "MainScene" with the name of your main scene)
        SceneManager.LoadScene("main");
    }
}
