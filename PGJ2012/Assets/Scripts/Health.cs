using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	
	public int HealthAmount = 25;
	GameObject cam;
	GameObject player;
	GameObject sfx;
	
	// Use this for initialization
	void Start () {
		cam = GameObject.Find("Main Camera");
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
			sfx.GetComponent<sfxPlayer>().Play(4);
			PlayerController playerController;
			playerController = cam.GetComponent<PlayerController>();
			playerController.CurrentHealth += HealthAmount;
			Destroy(gameObject);
		}
	}
}
