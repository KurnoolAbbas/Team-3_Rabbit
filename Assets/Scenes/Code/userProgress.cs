using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UserProgress : MonoBehaviour
{
    public Text[] heading1; // Array to store heading texts
    public Text[] heading2;

    public Text[] heading3;

    public Text[] heading4;

    public Text[] heading5;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(LoadUserData());

    }


    void SetTextValues(Text[] headings, string newName, string newGrade, int newScore, float newTime)
    {
        newTime = PlayerPrefs.GetFloat("time", 0);

        Debug.Log("time is " + newTime);
        headings[0].text = newName;
        headings[1].text = newScore.ToString();
        headings[2].text = newTime.ToString("F2"); // Format time to 2 decimal places

        string grade;
        if (newScore == 5)
        {
            grade = "A";
        }
        else if (newScore == 3 || newScore == 4)
        {
            grade = "B";
        }
        else
        {
            grade = "C";
        }

        headings[3].text = grade;




    }


    // Example function to update values
    IEnumerator LoadUserData()
    {
        // string userID = PlayerPrefs.GetString("userID");
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