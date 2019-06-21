using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HighscoreManager : MonoBehaviour {
	
	public struct Score
	{
		public int score;
	}
	
	public class MultiplierTimer
	{
		public int MultiplierAmount;
		public float Duration;
	}
	
	Score score;
	PlayerController playerController;
	
	GameObject game;
	
	private List<MultiplierTimer> multipliers = new List<MultiplierTimer>();
	
	public int Multiplier = 1;
	
	// Use this for initialization
	void Start () {
	
		playerController = GetComponent<PlayerController>();
		game = GameObject.Find("Game");
	}
	
	// Update is called once per frame
	void Update () {
		
		for(int i=0; i < multipliers.Count; i++)
		{
			multipliers[i].Duration -= Time.deltaTime;
			
			if(multipliers[i].Duration <= 0)
			{
				Multiplier /= multipliers[i].MultiplierAmount;
				multipliers.RemoveAt(i);
			}
		}
	
	}
	public int GetScore()
	{
		return score.score;
	}
	
	public void Clear()
	{
		multipliers.Clear();
	}
	
	void AddPoints(int points)
	{
		score.score += points * Multiplier; 
	}
	
	public void AddMultiplier(int multiplierAmount, float duration)
	{
		multipliers.Add(new MultiplierTimer() { Duration = duration * 10, MultiplierAmount = multiplierAmount });
		Multiplier *= multiplierAmount;
	}
	
	void OnGUI() {
		string multiplier = string.Empty;
		
		if(Multiplier > 1)
			multiplier = "x" + Multiplier.ToString();
		
		GUILayout.Label(string.Format("SCORE: {0} {1}", score.score, multiplier ));
		GUILayout.Label(string.Format("HIGHSCORE: {0} {1}", game.GetComponent<Game>().Highscore, game.GetComponent<Game>().PlayerName ));
		GUILayout.Label(string.Format("HEALTH {0}/{1}",playerController.CurrentHealth, playerController.MaxHealth));
	}
}
