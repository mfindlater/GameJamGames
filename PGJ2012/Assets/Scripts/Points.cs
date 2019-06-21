using UnityEngine;
using System.Collections;

public class Points : MonoBehaviour {
	
	public int PointsAmount = 100;
	GameObject player;
	GameObject cam;
	GameObject sfx;
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find("player_ball");
		cam = GameObject.Find("Main Camera");
		sfx = GameObject.Find("sfxPlayer");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject == player)
		{
			sfx.GetComponent<sfxPlayer>().Play(16);
			
			cam.BroadcastMessage("AddPoints", PointsAmount);
			
			Destroy(gameObject);
		}
	}
}
