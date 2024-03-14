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
    public GameObject pauseMenuPanel; // Reference to your pause menu panel

    private int currentQuestionIndex = 0;
    private int correctAnswersCount = 0;
    private int totalQuestions = 5;
    private int[] answerPositions = { 0, 1, 2 };
    private int previousCorrectOption = -1;
    private bool gamePaused = false;
    private bool gameEnded = false;

    private void Start()
    {
        DisplayNextQuestion();
    }

    private void DisplayNextQuestion()
    {
        if (currentQuestionIndex >= totalQuestions)
        {
            // Game over
            pauseMenuPanel.SetActive(false);
            lastText.text = "Game Over :)";
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

        if (isCorrect)
        {
            result.text = "Correct!";
            result.color = Color.green;
            correctAnswersCount++;
        }
        else
        {
            result.text = "Incorrect!";
            result.color = Color.red;
        }

        currentQuestionIndex++;
        StartCoroutine(DisplayNextQuestionAfterDelay());
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
        if (correctAnswersCount > 0)
        {
            result.text = "Correct: " + correctAnswersCount + "/" + totalQuestions;
            result.color = Color.green;
        }
        else
        {
            result.text = "Correct: " + correctAnswersCount + "/" + totalQuestions;
            result.color = Color.red;
        }

        // Hide the pause button if the game is ended
        pauseMenuPanel.SetActive(false);
        gameEnded = true;
    }
    public class SpawnRabbits : MonoBehaviour
    {
        public GameObject rabbitPrefab;
        public int answer;

        void Start()
        {
            SpawnRabbitStack(answer);
        }

        void SpawnRabbitStack(int numberOfRabbits)
        {
            for (int i = 0; i < numberOfRabbits; i++)
            {
                // Spawn a rabbit at the desired position
                Instantiate(rabbitPrefab, new Vector3(0, i * 1.0f, 0), Quaternion.identity);
            }
        }
    }
}