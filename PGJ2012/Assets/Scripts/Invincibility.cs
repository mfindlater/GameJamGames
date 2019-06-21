using UnityEngine;
using System.Collections;

public class Invincibility : MonoBehaviour {
	
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
			sfx.GetComponent<sfxPlayer>().Play(6);
			PlayerController playerController = cam.GetComponent<PlayerController>();
			playerController.BeginInvincibility();
			Destroy(gameObject);
		}
	}
}
