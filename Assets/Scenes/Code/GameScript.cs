using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public static class GameScript
{
    public static string baseAzureFunctionUrl = "https://team12.azurewebsites.net/api/game/";
    public static string azureFunctionAuthenticationParams = "code=VdxxDNNDxHgb-v0q8TcXSLV6x9W9TGYzyTxCAOcNpffNAzFuZ0O47g==&clientId=default";


    public static IEnumerator UpdateUserResponse(string gameID, bool validUserResponse, System.Action<bool> onSuccess, System.Action<string> onError)
    {
        string endpoint = $"{gameID}/update";
        string queryParams = $"?{azureFunctionAuthenticationParams}&validUserResponse={validUserResponse}";
        string fullUrl = baseAzureFunctionUrl + endpoint + queryParams;


        Debug.Log($"Updating user response as: {validUserResponse} at {fullUrl}");


        using (UnityWebRequest www = UnityWebRequest.Post(fullUrl, new WWWForm()))
        {
            yield return www.SendWebRequest();


            if (www.result != UnityWebRequest.Result.Success)
            {
                string errorMessage = $"Failed to update user response: {www.error}";
                //Debug.LogError(errorMessage);
                onError?.Invoke(errorMessage);
            }
            else
            {
                Debug.Log("User response updated successfully");
                onSuccess?.Invoke(true);
            }
        }
    }






    // public static IEnumerator UpdateGameCompletedStats(string gameId, double accuracy, double completion, System.Action<bool> onSuccess, System.Action<string> onError)
    // {
    //     string endpoint = $"{gameId}/complete";
    //     string queryParams = $"?{azureFunctionAuthenticationParams}&accuracy={accuracy}&completion={completion}";
    //     string url = $"{baseAzureFunctionUrl}{endpoint}{queryParams}";


    //     Debug.Log($"Updating game completed stats: {url}");


    //     using (UnityWebRequest www = UnityWebRequest.Post(url, new WWWForm()))
    //     {
    //         yield return www.SendWebRequest();


    //         if (www.result != UnityWebRequest.Result.Success)
    //         {
    //             string errorMessage = $"Failed to call API: {www.error}";
    //             Debug.LogError(errorMessage);
    //             onError?.Invoke(errorMessage);
    //         }
    //         else
    //         {
    //             Debug.Log("API call successful");
    //             onSuccess?.Invoke(true);
    //         }
    //     }
    // }


    public static IEnumerator GetUserHighestScore(string userId, System.Action<List<Game>> onSuccess, System.Action<string> onError)
    {
        Debug.Log("Calling Get User Score");
        string endpoint = $"getHighestGameStats";
        string queryParams = $"?{azureFunctionAuthenticationParams}&userID={userId}";

        string url = $"{baseAzureFunctionUrl}{queryParams}";


        Debug.Log($"Fetching game stats: {url}");


        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                string errorMessage = $"Failed to call API: {www.error}";
                Debug.LogError(errorMessage);
                onError?.Invoke(errorMessage);
            }
            else
            {
                string responseBody = www.downloadHandler.text;
                Debug.Log("Received JSON data: " + responseBody);

                ApiResponse response = JsonUtility.FromJson<ApiResponse>(responseBody);
                ApiResponses data = JsonUtility.FromJson<ApiResponses>(response.Content);
                List<Game> gamesList = new List<Game>();


                if (data != null && data.games != null)
                {
                    foreach (Game game in data.games)
                    {
                        Debug.Log("Name" + game.Name);
                        Debug.Log("Game ID: " + game.gameId);
                        Debug.Log("User ID: " + game.userId);
                        Debug.Log("Correct Answers: " + game.noOfCorrectAnswers);
                        Debug.Log("Wrong Answers: " + game.noOfWrongAnswers);
                        Debug.Log("Game Completed: " + game.gameCompleted);
                        Debug.Log("Accuracy Rate: " + game.accuracyRate);
                        Debug.Log("Completion Rate: " + game.completionRate);
                        gamesList.Add(game);
                    }
                }
                else
                {
                    Debug.LogWarning("Game object is null.");
                }



                onSuccess?.Invoke(gamesList);
            }
        }
    }

    public static IEnumerator UpdateGameCompletedStats(string gameId, double accuracy, double completion, System.Action<bool> onSuccess, System.Action<string> onError)
    {
        string endpoint = $"{gameId}/complete";
        string queryParams = $"?{azureFunctionAuthenticationParams}&accuracy={accuracy}&completion={completion}";
        string url = $"{baseAzureFunctionUrl}{endpoint}{queryParams}";


        Debug.Log($"Updating game completed stats: {url}");


        byte[] postData = System.Text.Encoding.UTF8.GetBytes("");

        using (UnityWebRequest www = UnityWebRequest.Put(url, postData))
        {
            yield return www.SendWebRequest();


            if (www.result != UnityWebRequest.Result.Success)
            {
                string errorMessage = $"Failed to call API: {www.error}";
                //Debug.LogError(errorMessage);
                onError?.Invoke(errorMessage);
            }
            else
            {
                Debug.Log("API call successful");
                onSuccess?.Invoke(true);
            }
        }
    }




    public static IEnumerator CreateNewGame(string userID, System.Action<string> onSuccess, System.Action<string> onError)
    {
        string endpoint = "create";
        string queryParams = $"{azureFunctionAuthenticationParams}&userID={userID}";
        string url = $"{baseAzureFunctionUrl}{endpoint}?{queryParams}";
        Debug.Log("Creating New Game" + url);


        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        UnityWebRequest www = UnityWebRequest.Post(url, formData);
        yield return www.SendWebRequest();


        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to create game: " + www.error);
        }
        else
        {
            string responseBody = www.downloadHandler.text;
            Debug.Log("Received JSON data: " + responseBody);
            try
            {
                // Assuming apiResponse is the JSON string received from the API
                ApiResponse response = JsonUtility.FromJson<ApiResponse>(responseBody);
                Database data = JsonUtility.FromJson<Database>(response.Content);
                string gameID = data.gameId;
                onSuccess?.Invoke(gameID);
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error parsing JSON data: " + ex.Message;
                Debug.LogError(errorMessage);
                onError?.Invoke(errorMessage);
            }
        }
    }
}