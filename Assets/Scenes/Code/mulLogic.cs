using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mulLogic : MonoBehaviour
{
    public Text lastText;
    public Text result;
    public Text questionText;
    public Button[] optionButtons;
    public Text questionCounterText;
    public GameObject pauseMenuPanel;
    public GameObject endGamePanel;
    public Text accuracyText;
    public Text rateText;
    public Text wrongText;


    private int currentQuestionIndex = 0;
    private int correctAnswersCount = 0;
    private int totalQuestions = 5;
    private int[] answerPositions = { 0, 1, 2 };
    private int previousCorrectOption = -1;
    private bool gamePaused = false;
    private bool gameEnded = false;

    private float startTime;
    private float endTime;

    private void Start()
    {
        DisplayNextQuestion();
        startTime = Time.time;
    }

    private void DisplayNextQuestion()
    {
        ResetButtonColors();

        if (currentQuestionIndex >= totalQuestions)
        {
            // To display the results when the game ends
            pauseMenuPanel.SetActive(false);
            lastText.text = "Congratulations";
            DisplayFinalResult();
            DisableOptionButtons();
            return;
        }

        int num1, num2;
        do
        {
            num1 = Random.Range(1, 11);
            num2 = Random.Range(1, 11);
        } while (num1 * num2 == previousCorrectOption || (currentQuestionIndex > 0 && currentQuestionIndex % 3 == 0 && num1 * num2 == previousCorrectOption));

        previousCorrectOption = num1 * num2;

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
                int randomAnswer;
                do
                {
                    randomAnswer = Random.Range(1, 101); // Generate random wrong answers
                } while (randomAnswer == correctAnswer || AnswerExists(randomAnswer));

                optionButtons[i].GetComponentInChildren<Text>().text = randomAnswer.ToString();
            }
        }

        questionCounterText.text = (currentQuestionIndex + 1) + "/" + totalQuestions;

        EnableOptionButtons(); // Ensure buttons are enabled for each question
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

    private bool AnswerExists(int answer)
    {
        foreach (Button button in optionButtons)
        {
            if (button.GetComponentInChildren<Text>().text == answer.ToString())
            {
                return true;
            }
        }
        return false;
    }

    public void CheckAnswer(int selectedAnswerIndex)
    {
        if (gameEnded || gamePaused)
            return; // If the game is paused or ended, do not process answer

        int correctAnswerIndex = answerPositions[0];
        bool isCorrect = selectedAnswerIndex == correctAnswerIndex;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            Button button = optionButtons[i];
            Text buttonText = button.GetComponentInChildren<Text>();

            if (i == correctAnswerIndex && !isCorrect)
            {
                buttonText.color = Color.white;
            }
            else if (i == selectedAnswerIndex)
            {
                buttonText.color = isCorrect ? Color.green : Color.red;
            }
            else
            {
                buttonText.color = Color.white;
            }

            button.interactable = false;
        }

        if (!isCorrect)
        {
            result.text = "Incorrect!";
            result.color = Color.red;
        }
        else
        {
            result.text = "Correct!";
            result.color = Color.green;
            correctAnswersCount++;
        }

        currentQuestionIndex++;
        StartCoroutine(DisplayNextQuestionAfterDelay());
    }
    private void ResetButtonColors()
    {
        foreach (Button button in optionButtons)
        {
            Text buttonText = button.GetComponentInChildren<Text>();
            buttonText.color = Color.white; // Reset color to black for all buttons
        }
    }


    private IEnumerator DisplayNextQuestionAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);

        result.text = "";

        DisplayNextQuestion();

        EnableOptionButtons();
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

    private void EnableOptionButtons()
    {
        foreach (Button button in optionButtons)
        {
            button.interactable = true;
        }
    }

    public void PauseGame()
    {
        gamePaused = true;
        pauseMenuPanel.SetActive(true); // Show the pause menu panel
        Time.timeScale = 0f; // Pause the game
    }

    public void ResumeGame()
    {
        gamePaused = false;
        pauseMenuPanel.SetActive(false); // Hide the pause menu panel
        Time.timeScale = 1f; // Resume the game
    }

    public void RestartGame()
    {
        currentQuestionIndex = 0;
        correctAnswersCount = 0;
        gamePaused = false;
        pauseMenuPanel.SetActive(false); // Hide the pause menu panel
        Time.timeScale = 1f; // Resume the game
        DisplayNextQuestion();
    }

    public void quit()
    {
        SceneManager.LoadScene("main");
    }

    private void DisplayFinalResult()
    {

        float accuracy = (float)correctAnswersCount / totalQuestions * 100;
        endTime = Time.time;
        float rate = totalQuestions / (endTime - startTime) * 60; // Calculate rate per minute
        int wrongAnswers = totalQuestions - correctAnswersCount;


        int roundedAccuracy = Mathf.CeilToInt(accuracy);
        int roundedRate = Mathf.CeilToInt(rate);

        // Display stats with rounded values
        accuracyText.text = "Accuracy: " + roundedAccuracy + "%";
        rateText.text = "Rate: " + roundedRate + "/min";
        wrongText.text = "Wrong: " + wrongAnswers;

        GameObject pauseButton = GameObject.Find("PauseButton");
        if (pauseButton != null)
        {
            pauseButton.SetActive(false);
        }

        pauseMenuPanel.SetActive(false);
        gameEnded = true;

        // Show end game panel
        endGamePanel.SetActive(true);
    }
    public void RestartFromQuestions()
    {
        SceneManager.LoadScene("multScreen");
    }
    public void HomeScreen()
    {
        SceneManager.LoadScene("main");
    }


}
