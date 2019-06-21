using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
	
	GameObject game;
	int hs;
	int s;
	string hsName;
	
	public Texture GameOverScreen;
	
	
	// Use this for initialization
	void Start () {
		game = (GameObject)GameObject.Find("Game");
		hs = game.GetComponent<Game>().Highscore;
		s = game.GetComponent<Game>().Score;
		hsName = game.GetComponent<Game>().PlayerName; 
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), GameOverScreen);
		if(s > hs)
		{
			GUILayout.Label("NEW RECORD!");
			GUILayout.Label(string.Format("{0} {1} NEW!",s,hsName));
			GUILayout.Label("Enter your Name:");
			hsName = GUILayout.TextField(hsName);
			game.GetComponent<Game>().Highscore = s;
			game.GetComponent<Game>().PlayerName = hsName;
			
		}
		
		float centerX = Screen.width / 2;
		if(GUI.Button(new Rect(centerX-128, 100, 128, 32), "Play Again?"))
		{
			Application.LoadLevel("Gameplay");
		}
	}
}
