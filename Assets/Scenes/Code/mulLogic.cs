using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class mulLogic : MonoBehaviour
{

    
    public Text lastText;
    public Text result;

    public Text questionText;
    public Button[] optionButtons;
    public Text questionCounterText;

    private int currentQuestionIndex = 0;
    private int correctAnswersCount = 0;
    private int totalQuestions = 5;
    private int[] answerPositions = { 0, 1, 2 };

    private void Start()
    {
        DisplayNextQuestion();
    }

    
    private void DisplayNextQuestion()
    {
        if (currentQuestionIndex >= totalQuestions)
        {
            // Game over
            lastText.text = "Game Over :)";
            result.text = "Correct: " + correctAnswersCount + "/" + totalQuestions;
            DisableOptionButtons();
            return;
        }

        int num1 = Random.Range(1, 11);
        int num2 = Random.Range(1, 11);
        int correctAnswer = num1 * num2;

        questionText.text = num1 + " * " + num2 + " = ?";

        ShuffleAnswerPositions();

        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i == answerPositions[0])
            {
                optionButtons[i].GetComponentInChildren<Text>().text = correctAnswer.ToString();
            }
            else
            {
                int randomAnswer = Random.Range(1, 101); // Generate random wrong answers
                optionButtons[i].GetComponentInChildren<Text>().text = randomAnswer.ToString();
            }
        }

        questionCounterText.text = (currentQuestionIndex + 1) + "/" + totalQuestions;
    }


    private void ShuffleAnswerPositions()
    {
        for (int i = 0; i < answerPositions.Length; i++)
        {
            int temp = answerPositions[i];
            int randomIndex = Random.Range(i, answerPositions.Length);
            answerPositions[i] = answerPositions[randomIndex];
            answerPositions[randomIndex] = temp;
        }
    }

    public void CheckAnswer(int selectedAnswerIndex)
    {
        
        int correctAnswerIndex = answerPositions[0];
        if (selectedAnswerIndex == correctAnswerIndex)
        {
            result.text = "Correct!";
            //Debug.Log("Correct!");
            correctAnswersCount++;
        }
        else
        {
            result.text = "InCorrect!";
            //Debug.Log("Incorrect!");
        }

        currentQuestionIndex++;
        StartCoroutine(DisplayNextQuestionAfterDelay());
    }
    private IEnumerator DisplayNextQuestionAfterDelay()
    {
        // Wait for a brief delay before displaying the next question
        yield return new WaitForSeconds(1.5f);

        // Clear the result text
        result.text = "";

        // Display the next question
        DisplayNextQuestion();
    }

    private void DisableOptionButtons()
    {
        questionCounterText.gameObject.SetActive(false);
        questionText.gameObject.SetActive(false);
        foreach (Button button in optionButtons)
        {
            button.gameObject.SetActive(false);
        }
    }
   
}