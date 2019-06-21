using UnityEngine;
using System.Collections;

public class kill_script : MonoBehaviour {
	public int DamageValue = 20;
	GameObject ball;
	GameObject camObj;
	// Use this for initialization
	void Start () {
		ball = GameObject.Find("player_ball");
		camObj = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
        if(other.gameObject == ball)
		{
			camObj.BroadcastMessage("TakeDamage", DamageValue);
			camObj.BroadcastMessage("Respawn");
			camObj.BroadcastMessage("EndInvincibility");
		}
    }
}
