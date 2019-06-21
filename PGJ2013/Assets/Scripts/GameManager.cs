using UnityEngine;
using System.Collections;

public class GameManager 
{

    public static bool[] ActivePlayers = new bool[4];
    public static int[] PlayerActions = new int[4];
    public static string Winners = string.Empty;
    public static double PlayerSync;
    public static string PlayerRank;

    public static string[] ranks = new string[] { "A", "A", "B","B", "B","C", "C", "C", "D", "D", "D", "D", "S" };

    public static void DetermineRank()
    {
        PlayerRank = ranks[Random.Range(0, ranks.Length)];
    }

    public static void DetermineSync(int amt1, int amt2)
    {
        double highest = 0;
        double lowest = 0;
       
        if (amt1 > amt2)
        {
            highest = amt1;
            lowest = amt2;
        }
        else if (amt2 > amt1)
        {
            highest = amt2;
            lowest = amt1;
        }
        else
        {
            highest = amt1;
            lowest = amt2;
        }

        PlayerSync = (lowest / highest) * 100;
    }

}
