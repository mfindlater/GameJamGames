using UnityEngine;
using System.Collections;

public class BallCollision : MonoBehaviour {
	
	GameObject cam;
	PlayerController playerController;
	
	// Use this for initialization
	void Start () {
		
		cam = GameObject.Find("Main Camera");
		playerController = cam.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {

	
	}
	
	void OnCollisionEnter(Collision collision)
	{	
		if(playerController.IsInvincible)
		{
			
			
			if(string.Equals(collision.gameObject.tag, "Obstacle"))
			{
				GameObject.Destroy(collision.gameObject);
				cam.BroadcastMessage("AddPoints", 100);
				//Destroy Obstacle with particle effects.
			}
		}
	}
}
