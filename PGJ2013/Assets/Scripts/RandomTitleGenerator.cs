using UnityEngine;
using System.Collections;

public class RandomTitleGenerator : MonoBehaviour {
    public static string CurrentTitle;
    public static string[] Words1 = new string[] { "HYPER", "SUPER", "MEGA", "SUPA", "DARK", "G", "NEO", "ULTRA", "DAI", "SHIN", "ATOMIC", "BLACK", "CODE", "GIANT", "GRAND", "NEW", "ARMORED", "DATA", "COLD", "GOLD", "TITANIUM", "SILVER", "VIRTUAL", "CHAOS", "FATAL", "MIGHTY", "VIRTUA", "SPACE", "PHILLY" };


    public static string[] Words2 = new string[] { "ROBOT", "ROBO", "MACHINE", "MECHANIZER", "BIO-MACHINE", "MECHA", "BOT", "BIORIZER", "GHOSTERIZER", "NO", "GAI", "METAL", "STEEL", "BATTLE", "KING", "TITAN", "TECH", "FIGHTER" };


    public static string[] Words3 = new string[] { "PANIC", "GHOST", "DX", "4K", "Z", "X", "DIMENSION", "2", "SHELL", "L", "V", "VIPER", "QUAD", "INFINITE", "BLACK OPS", "BOLT", "GAIDEN", "4", "GAI", "REVENGE", "RISING", "FLASH", "LOTUS", "FANG", "COMBAT", "RED", "R", "DUAL", "SWORD", "CROSS", "CORE", "KATANA", "LANCE", "LANCER", "SQUAD", "ON", "SHADOW", "DREAM", "NIGHTMARE", "ASSAULT", "KAISER", "ZONE", "EX", "TURBO", "FURY", "NEPTUNE", "NEPTUNIA", "CUBED", "JAM", "GOD", "GODS", "EXREME", ""};
    public static string GetTitle()
    {
        string word1 = Words1[Random.Range(0, Words1.Length)];
        string word2 = Words2[Random.Range(0, Words2.Length)];
        string word3 = Words3[Random.Range(0, Words3.Length)];
        string s = string.Format("{0} {1} {2}", word1, word2, word3);
        CurrentTitle = s;
        return s;
    }

    public static string[] Words4 = new string[] { "FIGHT!", "HAJIME!", "LET'S ROCK!", "AMERICA!", "GO!", "HAVE AT IT!", "USE YOUR ROBOT FISTS!", "DUEL!", "DESTROY!", "LIVE AND LET DIE"};
    public static string GetReady()
    {
        return Words4[Random.Range(0, Words4.Length)];
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
