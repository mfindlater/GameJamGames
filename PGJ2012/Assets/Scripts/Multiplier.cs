using UnityEngine;
using System.Collections;

public class Multiplier : MonoBehaviour {
	
	public int MultiplierAmount = 2;
	public bool IsPersistant = false;
	public float Duration = 5;
	GameObject player;
	GameObject cam;
	GameObject sfx;
	
	HighscoreManager highScoreManager;
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find("player_ball");
		cam = GameObject.Find("Main Camera");
		sfx = GameObject.Find("sfxPlayer");
		
		highScoreManager = cam.GetComponent<HighscoreManager>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject == player)
		{
			sfx.GetComponent<sfxPlayer>().Play(8);
			
			highScoreManager.AddMultiplier(MultiplierAmount, Duration);
			Destroy(gameObject);
		}
	}
	
	
}
