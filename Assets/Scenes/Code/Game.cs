using UnityEngine;
using System;


[Serializable]
public class ApiResponses
{
    public Game[] games;
}

[Serializable]
public class Game
{
    public string gameId;
    public string userId;

    public string Name;
    public int noOfCorrectAnswers;
    public int noOfWrongAnswers;
    public bool gameCompleted;
    public double accuracyRate;
    public double completionRate;
}