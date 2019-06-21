using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class Game : MonoBehaviour {
	
	public string PlayerName = "AHALL";
	public int Highscore = 100;
	public int Score = 0;
	
	// Use this for initialization
	void Start () {
		Object.DontDestroyOnLoad(gameObject);
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
