using UnityEngine;
using System.Collections;
using System;

public class Score : MonoBehaviour {

	private int currentScore;
	public int maxLives = 3;
	private int currentLives;
	private float timeRemaining;
	private float startTime;
	public float maxTime = 180;
	private Health health;
	public Action GameOver;
	private bool gameIsOver;

	public int CurrentScore
    {
        get { return currentScore; }
        set { currentScore = value; }
    }

	public int CurrentLives
    {
        get { return currentLives; }
        set { currentLives = value; }
    }

    public float TimeRemaining
    {
        get { return timeRemaining; }
        set { timeRemaining = value; }
    }

	// Use this for initialization
	void Start () {
		gameIsOver = false;
		currentLives = maxLives;
		timeRemaining = maxTime;
		startTime = Time.time;
		GameWorld.GameOver = false;
		currentScore = 0;
		health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
		health.HealthReachedZero += OnPlayerDeath;
		GameOver += OnGameOver;
	}	
	
	// Update is called once per frame
	void Update () {
		timeRemaining = maxTime - (Time.time - startTime);
		if (timeRemaining <= 0 && !gameIsOver)
		{
			if (GameOver != null)
                GameOver();
		}
	}

	void OnPlayerDeath()
	{
		currentLives -= 1;
		print("PLAYYER DIIEEED");
		if (currentLives < 1)
		{
			if (GameOver != null)
                GameOver();
		}
	}
	void OnGameOver()
	{
        //iTween.Stop("shake");
		gameIsOver = true;
		print("GAME OVER");
		GameWorld.GameOver = true;
		CompareHighScore();
		iTween.Stop();
		Time.timeScale = 0;
	}

	void CompareHighScore()
	{

		ProfileManager pManager = null;
		if (GameObject.Find("Profile_Manager") != null)
			pManager = GameObject.Find("Profile_Manager").GetComponent<ProfileManager>();
		if (pManager != null && currentScore > pManager.playerProf.highScore)
		{
			pManager.playerProf.highScore = currentScore;
			pManager.SaveProfile();
		}
	}
}
