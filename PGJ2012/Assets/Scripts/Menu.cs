using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
	public Texture Title;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetButtonDown("Start_1") || Input.GetButtonDown("A_1") || Input.GetKeyDown(KeyCode.Return))
		{
			Application.LoadLevel("Gameplay");
		}
	
	}
	
	void OnGUI()
	{
		float centerX = Screen.width / 2;
		float bottomY = Screen.height - 64;	
		GameObject go = GameObject.Find("Game");
		int hs = go.GetComponent<Game>().Highscore;
		GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), Title); 
		GUILayout.Label(string.Format("HIGHSCORE: {0} {1}",hs.ToString(), go.GetComponent<Game>().PlayerName));
		if(GUI.Button(new Rect(centerX-128, 125, 128, 32), "PLAY"))
		{
			Application.LoadLevel("Gameplay");
		}
		GUI.Label(new Rect(0, bottomY, 256, 64),"Created By John Benge, Matthew Findlater, Stefan Lopuszanski, Du-Marc Mills");
	}
}
