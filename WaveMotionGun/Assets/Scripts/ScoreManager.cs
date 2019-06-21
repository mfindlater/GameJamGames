using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITallyListener
{
    void OnTally(int tally);
}

public static class ScoreManager
{
    private const int multiplier = 2;

    public static List<ITallyListener> Listeners = new List<ITallyListener>();

    static ScoreManager()
    {
        scoreValues = new Dictionary<EnemyType, int>();
        scoreValues.Add(EnemyType.Kamikaze, 100);
        scoreValues.Add(EnemyType.Straightforward, 100);
        scoreValues.Add(EnemyType.StopAndShoot, 500);
        scoreValues.Add(EnemyType.Wildcard, 250);
    }

    public static void Initialize()
    {
        score = 0;
    }

    public static int Tally { get; private set; }

    public static void SetTally(int tally)
    {
        Tally = tally;
        OnTally(tally);
    }

    public static int Score { get { return score; } }

    private static int score = 0;

    private static Dictionary<EnemyType, int> scoreValues = new Dictionary<EnemyType, int>();
    
    public static void AddScore(EnemyType enemyType)
    {
       score += scoreValues[enemyType];
    }

    public static void AddScore(List<EnemyType> enemyTypes)
    {
        int sumScores = 0;
        for(int i=0; i < enemyTypes.Count; i++)
        {
            sumScores += scoreValues[enemyTypes[i]];
        }

        score += sumScores * multiplier;
    }

    public static void OnTally(int tally)
    {
        for(int i=0; i < Listeners.Count; i++)
        {
            Listeners[i].OnTally(tally);
        }
    }
}
