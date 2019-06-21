using UnityEngine;
using System.Collections;

public class obstacle_damage : MonoBehaviour {

	// Use this for initialization
	public int DamageValue = 20;
	GameObject ball;
	GameObject camObj;
	public bool IsPersistent = true;
	GameObject sfx;
	
	// Use this for initialization
	void Start () {
		ball = GameObject.Find("player_ball");
		camObj = GameObject.Find("Main Camera");
		sfx = GameObject.Find("sfxPlayer");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision collision) {
        if(collision.gameObject == ball)
		{
			sfx.GetComponent<sfxPlayer>().Play(10);
			camObj.BroadcastMessage("TakeDamage", DamageValue);
			
			if(!IsPersistent)
				Destroy(gameObject);	
		}
    }
}
