using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MathNet.Numerics.Random;

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
    public GameObject correctImage; // Reference to the GameObject containing the correct image
    public GameObject incorrectImage; // Reference to the GameObject containing the incorrect image
    public AudioSource correctSound; // Assigned correct sound source
    public AudioSource incorrectSound; // Assigned incorrect sound source
    public GameObject starImage1;
    public GameObject starImage2;
    public GameObject starImage3;
    private int savedNum1;
    private int savedNum2;


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

        incorrectImage.SetActive(false);
        correctImage.SetActive(false);
        //ClearSavedValues();
        if (PlayerPrefs.HasKey("IsGameSaved"))
        {
            LoadGameState(); // If saved state exists, load it


        }
        DisplayNextQuestion();
        startTime = Time.time;
    }

    private void DisplayNextQuestion()
    {
        ResetButtonColors();
        var rng = new SystemRandomSource();
        int num1, num2;
        if (currentQuestionIndex >= totalQuestions)
        {
            // To display the results when the game ends
            pauseMenuPanel.SetActive(false);
            lastText.text = "";
            DisplayFinalResult();
            DisableOptionButtons();
            return;
        }

        int currentQuestionIndex1 = PlayerPrefs.GetInt("CurrentQuestionIndex", 0);

        if (PlayerPrefs.HasKey("IsGameSaved") && savedNum1 != 0 && savedNum2 != 0 && currentQuestionIndex1== currentQuestionIndex)
        {
            num1 = savedNum1;
            num2 = savedNum2;
            
            
        }
        else
        {
            Debug.Log("calling closed saved values");
            PlayerPrefs.DeleteKey("savedNum1");
            PlayerPrefs.DeleteKey("savedNum2");

            Debug.Log("savedNum1 " + savedNum1);

            Debug.Log("savedNum2 " + savedNum2);
            do
            {
                num1 = rng.Next(1, 4);
                num2 = rng.Next(1, 4);
            } while (num1 * num2 == previousCorrectOption || (currentQuestionIndex > 0 && currentQuestionIndex % 3 == 0 && num1 * num2 == previousCorrectOption && currentQuestionIndex1 != currentQuestionIndex));
            
        }
        savedNum1 = 0;
        savedNum2 = 0;
        PlayerPrefs.SetInt("savedNum1", savedNum1);
        PlayerPrefs.SetInt("savedNum2", savedNum2);
        Debug.Log("AsavedNum1 " + savedNum1);

        Debug.Log("AsavedNum2 " + savedNum2);
        previousCorrectOption = num1 * num2;

        FindObjectOfType<QuestionAudio>().AudioPlayAsync(num1, num2);

        int correctAnswer = num1 * num2;
        Debug.Log("num1 " + num1);

        Debug.Log("num2 " + num2);
        GenerateGrid(num1, num2);

        questionText.text = num1 + " * " + num2 + " = ?";

        ShuffleAnswerPositions(rng);

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
                    randomAnswer = rng.Next(1, 101); // Generate random wrong answers
                } while (randomAnswer == correctAnswer || AnswerExists(randomAnswer));

                optionButtons[i].GetComponentInChildren<Text>().text = randomAnswer.ToString();
            }
        }

        questionCounterText.text = (currentQuestionIndex + 1) + "/" + totalQuestions;

        EnableOptionButtons(); // Ensure buttons are enabled for each question
        savedNum1 = num1;
        savedNum2 = num2;
        ClearSavedValues();
        Debug.Log("savedNum11out " + savedNum1);

        Debug.Log("savedNum22out " + savedNum2);

    }
    private void ClearSavedValues()
    {
        Debug.Log("in clear saved values");
        PlayerPrefs.DeleteKey("IsGameSaved");
        PlayerPrefs.DeleteKey("CurrentQuestionIndex");
        PlayerPrefs.DeleteKey("CorrectAnswersCount");
        
        Debug.Log("in clear saved values CurrentQuestionIndex"+ currentQuestionIndex);
        Debug.Log("in clear saved values CorrectAnswersCount"+ correctAnswersCount);
        

        // Add any other saved values to be cleared
    }
    private void GenerateGrid(int num1, int num2)
    {
        // Destroy(refereneceTile);
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        GameObject referenceTile = (GameObject)Instantiate(Resources.Load("prefab"));
        Canvas canvasRenderer = referenceTile.GetComponent<Canvas>();

        // Set the sorting order
        canvasRenderer.sortingOrder = 2;

        float initialPosX = 1828;
        float initialPosY = 1408;
        float rowSpacing = 200;
        float colSpacing = 200;

        for (int row = 0; row < num2; row++)
        {
            float posY = initialPosY + (row * rowSpacing);

            for (int col = 0; col < num1; col++)
            {
                float posX = initialPosX + (col * colSpacing);
                GameObject tile = (GameObject)Instantiate(referenceTile, transform);
                tile.transform.localPosition = new Vector2(posX, posY);
            }
        }

        Destroy(referenceTile); // Destroy the reference tile object

    }
    private void ShuffleAnswerPositions(SystemRandomSource rng)
    {
        for (int i = 0; i < answerPositions.Length; i++)
        {
            int temp = answerPositions[i];
            int randomIndex = rng.Next(i, answerPositions.Length);
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

        // Loop through each option button
        for (int i = 0; i < optionButtons.Length; i++)
        {
            Button button = optionButtons[i];
            Text buttonText = button.GetComponentInChildren<Text>();

            // If the current button is the correct answer
            if (i == correctAnswerIndex)
            {
                // Set the color to green if the selected answer is correct, and to red if it's incorrect
                buttonText.color = isCorrect ? Color.green : Color.red;
                button.interactable = true;
            }
            else
            {
                // Hide other buttons
                button.gameObject.SetActive(false);
            }
        }

        // Handle correct and incorrect answer feedback
        if (!isCorrect)
        {
            // If the answer is incorrect
            correctImage.SetActive(false);
            result.text = "";
            result.color = Color.red;
            incorrectImage.SetActive(true); // Enable the incorrect image GameObject
            incorrectSound.Play(); // Play incorrect sound effect
        }
        else
        {
            // If the answer is correct
            incorrectImage.SetActive(false);
            result.text = "";
            result.color = Color.green;
            correctAnswersCount++;
            correctImage.SetActive(true); // Enable the correct image GameObject
            correctSound.Play(); // Play correct sound effect
        }

        currentQuestionIndex++;
        StartCoroutine(DisplayNextQuestionAfterDelay());
    }



    private void ResetButtonColors()
    {
        foreach (Button button in optionButtons)
        {
            button.gameObject.SetActive(true);
            Text buttonText = button.GetComponentInChildren<Text>();
            buttonText.color = Color.black; // Reset color to black for all buttons
        }
    }


    private IEnumerator DisplayNextQuestionAfterDelay()
    {

        yield return new WaitForSeconds(1.5f);

        result.text = "";

        DisplayNextQuestion();
        incorrectImage.SetActive(false);
        correctImage.SetActive(false);

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
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        gamePaused = false;
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        currentQuestionIndex = 0;
        correctAnswersCount = 0;
        gamePaused = false;
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        DisplayNextQuestion();
    }

    public void quit()
    {

        SceneManager.LoadScene("main");
        currentQuestionIndex = 0;
        correctAnswersCount = 0;
        gamePaused = false;
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }


    private void DisplayFinalResult()
    {
        incorrectImage.SetActive(false);
        correctImage.SetActive(false);
        float accuracy = (float)correctAnswersCount / totalQuestions * 100;
        endTime = Time.time;
        float rate = totalQuestions / (endTime - startTime) * 60;
        int wrongAnswers = totalQuestions - correctAnswersCount;


        int roundedAccuracy = Mathf.CeilToInt(accuracy);
        int roundedRate = Mathf.CeilToInt(rate);

        accuracyText.text = "Accuracy: " + roundedAccuracy + "%";
        rateText.text = "Rate: " + roundedRate + "/min";
        wrongText.text = "Wrong: " + wrongAnswers;
        if (accuracy >= 1 && accuracy <= 50)
        {
            starImage1.SetActive(true);
        }
        else if (accuracy > 50 && accuracy <= 70)
        {
            starImage1.SetActive(true);
            starImage2.SetActive(true);
        }
        else if (accuracy > 70 && accuracy <= 100)
        {
            starImage1.SetActive(true);
            starImage2.SetActive(true);
            starImage3.SetActive(true);
        }

        GameObject pauseButton = GameObject.Find("PauseButton");
        if (pauseButton != null)
        {
            pauseButton.SetActive(false);
        }

        pauseMenuPanel.SetActive(false);
        gameEnded = true;

        endGamePanel.SetActive(true);
        ClearSavedValues();
        /*PlayerPrefs.DeleteKey("IsGameSaved");
        PlayerPrefs.DeleteKey("CurrentQuestionIndex");
        PlayerPrefs.DeleteKey("CorrectAnswersCount");
        PlayerPrefs.DeleteKey("savedNum1");
        PlayerPrefs.DeleteKey("savedNum2");*/
    }
    public void RestartFromQuestions()
    {
        SceneManager.LoadScene("multScreen");
    }
    public void HomeScreen()
    {
        SceneManager.LoadScene("main");
    }
    public void SaveGameState()
    {
        PlayerPrefs.SetInt("IsGameSaved", 1); // Set a flag to indicate game state is saved
        PlayerPrefs.SetInt("CurrentQuestionIndex", currentQuestionIndex);
        PlayerPrefs.SetInt("CorrectAnswersCount", correctAnswersCount);
        PlayerPrefs.SetInt("savedNum1", savedNum1);
        PlayerPrefs.SetInt("savedNum2", savedNum2);
    }

    public void LoadGameState()
    {
        currentQuestionIndex = PlayerPrefs.GetInt("CurrentQuestionIndex", 0);
        correctAnswersCount = PlayerPrefs.GetInt("CorrectAnswersCount", 0);
        savedNum1 = PlayerPrefs.GetInt("savedNum1", 0);
        savedNum2 = PlayerPrefs.GetInt("savedNum2", 0);


    }




}

