using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;

public class UserProgress : MonoBehaviour
{
    public Text[] heading1; // Array to store heading texts
    public Text[] heading2;

    public Text[] heading3;

    public Text[] heading4;

    public Text[] heading5;
    private String name1;
    //private int accuracyRate;
    private double completionRate;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadUserData());

    }

    // Function to set text values
    void SetTextValues(Text[] headings, string newName, string newGrade, int newScore, float accuracy)
    {
        headings[0].text = newName;
        String star = "";
       
        int accuracyRate = (int)accuracy;
        headings[3].text = accuracyRate.ToString();
        float correctAnswer= (accuracyRate*5) /100;
        
        headings[1].text = ((int)(correctAnswer)).ToString(); ;

        if (accuracyRate >= 1 && accuracyRate <= 50)
        {
            star = "C";
        }
        else if (accuracyRate > 50 && accuracyRate <= 70)
        {
            star = "B";
        }
        else if (accuracyRate > 70 && accuracyRate <= 100)
        {
            star = "A";
        }
        headings[2].text = star;

    }
    

    // Example function to update values
    IEnumerator LoadUserData()
    {
     
        string userID = PlayerPrefs.GetString("userID");

        yield return StartCoroutine(GameScript.GetUserHighestScore(userID,
            onSuccess: (success) =>
            {
                foreach (var game in success)
                {
                    Debug.Log("Name :" + game.Name);
                    Debug.Log("Game ID: " + game.gameId);
                    Debug.Log("User ID: " + game.userId);
                    Debug.Log("Correct Answers: " + game.noOfCorrectAnswers);
                    Debug.Log("Wrong Answers: " + game.noOfWrongAnswers);
                    Debug.Log("Game Completed: " + game.gameCompleted);
                    Debug.Log("Accuracy Rate: " + game.accuracyRate);
                    Debug.Log("Completion Rate: " + game.completionRate);

                    SetTextValues(heading1, game.userId, " ", game.noOfCorrectAnswers, (float)game.accuracyRate);
              
                }


            },
            onError: (error) =>
            {
                Debug.LogError("Failed to update game completed stats: " + error);
            }));

    }

    

    public void Exit()
    {
        SceneManager.LoadScene("main");
    }


}










