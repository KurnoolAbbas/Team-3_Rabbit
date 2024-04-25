using UnityEngine;
using Microsoft.CognitiveServices.Speech;
using System.Threading.Tasks;

public class QuestionAudio : MonoBehaviour
{
    // Set your subscription key and region
    private string subscriptionKey = "553c7a209a624c8298337a1f8e9b25f9";
    private string region = "westus3";
    private int  numb1=0;
    private int numb2 = 0;

    
    public async Task AudioPlayAsync(int num1,int num2)
    {
        // Set the key and region configuration
        var config = SpeechConfig.FromSubscription(subscriptionKey, region);

        // Create a speech synthesizer object
        using (var synthesizer = new SpeechSynthesizer(config))
        {
            // Get a hardcoded question (replace this with your actual logic to get questions)
            int numb1 = num1;
            int numb2 = num2;
            string question = $" {numb1} times {numb2}  equal to"; ;
    
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
