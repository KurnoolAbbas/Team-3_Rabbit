using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MultiplicationLogic : MonoBehaviour
{
    public TextMeshProUGUI questionText, accuracyText, wrongText;
    public Button[] answerButtons;
    public Button closeButton, restartButton, backButton, pauseButton, resumeButton, completeRestartButton, completeMenuButton;
    private int correctAnswerIndex;
    private int currentQuestionIndex = 0;
    private const int totalQuestions = 5;
    private int correctlyAnswered = 0;
    public GameObject pauseMenuPanel, completeGamePanel;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenuPanel.SetActive(false);
        completeGamePanel.SetActive(false);
        pauseButton.onClick.AddListener(PauseGame);
        closeButton.onClick.AddListener(ResumeGame);
        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartGame);
        backButton.onClick.AddListener(BackToMainMenu);
        completeRestartButton.onClick.AddListener(CompleteRestartGame);
        completeMenuButton.onClick.AddListener(BackToMainMenu);
        GenerateQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void BackToMainMenu()
    {
        SceneManager.LoadScene("HomeScreen");
    }
    void GenerateQuestion()
    {
        if(currentQuestionIndex >= totalQuestions)
        {
            foreach(var button in answerButtons)
            {
                button.onClick.RemoveAllListeners();
                button.GetComponent<Image>().color = Color.white;
            }
            CompleteGame();
        }
        else
        {
            foreach(var button in answerButtons)
            {
                button.onClick.RemoveAllListeners();
                button.GetComponent<Image>().color = Color.white;
            }

            int a = Random.Range(1,7);
            int b = Random.Range(1,7);
            int answer = a*b;
            questionText.text = $"{a} * {b} = ?";
            correctAnswerIndex = Random.Range(0, answerButtons.Length);
            HashSet<int> usedAnswers = new HashSet<int> {answer}; //To track unique answers
            for(int i = 0; i < answerButtons.Length; i++){
                int option;
                if(i==correctAnswerIndex){
                    option = answer;
                }else{
                    do{
                        option = Random.Range(1,36);
                    }while(usedAnswers.Contains(option));
                    usedAnswers.Add(option);
                }
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = option.ToString();
                int index = i;
                answerButtons[i].onClick.AddListener(() => answerSelected(index));
            }
        }
    }
    void answerSelected(int index)
    {
        bool isCorrect = index==correctAnswerIndex;
        answerButtons[index].GetComponent<Image>().color = isCorrect? Color.green : Color.red;
        StartCoroutine(ContinueAfterFeedback(isCorrect, index));
    }

    IEnumerator ContinueAfterFeedback(bool isCorrect, int index)
    {
        yield return new WaitForSeconds(1);
        if(isCorrect){
            correctlyAnswered++;
        }
        currentQuestionIndex++;
        GenerateQuestion();
    }

    void CompleteGame()
    {
        completeGamePanel.SetActive(true);
        accuracyText.text = "Accuracy: "+(((double)correctlyAnswered/totalQuestions)*100)+"%";
        wrongText.text = "Wrong: "+(totalQuestions-correctlyAnswered);
        DisableGameInputs();
    }
    void DisableGameInputs()
    {
        foreach(var button in answerButtons)
        {
            button.interactable = false;
        }
        pauseButton.interactable = false;
    }
    void EnableGameInputs()
    {
        foreach(var button in answerButtons)
        {
            button.interactable = true;
        }
        pauseButton.interactable = true;
    }
    void CompleteRestartGame()
    {
        currentQuestionIndex = 0;
        correctlyAnswered = 0;
        completeGamePanel.SetActive(false);
        EnableGameInputs();
        GenerateQuestion();
    }
    void RestartGame()
    {
        currentQuestionIndex = 0;
        correctlyAnswered = 0;
        ResumeGame();
    }
    void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        EnableGameInputs();
    }
    void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        DisableGameInputs();
    }
}
