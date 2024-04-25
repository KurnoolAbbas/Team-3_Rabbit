using UnityEngine;
using Microsoft.CognitiveServices.Speech;
using System.Threading.Tasks;

public class TextToSpeech : MonoBehaviour
{
    // Set your subscription key and region
    private string subscriptionKey = "553c7a209a624c8298337a1f8e9b25f9";
    private string region = "westus3";

    private async void Start()
    {
        // Set the key and region configuration
        var config = SpeechConfig.FromSubscription(subscriptionKey, region);

        // Create a speech synthesizer object
        using (var synthesizer = new SpeechSynthesizer(config))
        {
            // Get a hardcoded question (replace this with your actual logic to get questions)
            string question = "3 times 2 equal";

            // Convert the question to speech
            var result = await synthesizer.SpeakTextAsync(question);

            // Check if the synthesis was successful
            if (result.Reason == ResultReason.SynthesizingAudioCompleted)
            {
                Debug.Log("Speech synthesis succeeded!");
            }
            else
            {
                Debug.LogError($"Speech synthesis failed: {result.Reason}");
            }
        }
    }
}
