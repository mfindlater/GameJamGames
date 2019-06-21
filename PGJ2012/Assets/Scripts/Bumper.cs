using UnityEngine;
using System.Collections;

public class Bumper : MonoBehaviour {
	GameObject player;
	GameObject sfx;
	// Use this for initialization
	void Start () {
		player = GameObject.Find("player_ball");
		sfx = GameObject.Find("sfxPlayer");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject == player)
		{
			sfx.GetComponent<sfxPlayer>().Play(1);
		}
	}
}
