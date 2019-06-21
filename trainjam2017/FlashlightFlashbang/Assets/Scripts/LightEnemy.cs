using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEnemy : MonoBehaviour {

    private PlayerBehavior player;

	// Use this for initialization
	void Awake () {
        player = transform.parent.parent.parent.GetComponent<PlayerBehavior>();
    }

	void OnTriggerEnter (Collider other){

		if(other.CompareTag("Enemy") && player.flashLightState.On){
			Destroy(other.gameObject);
			//
	       	Debug.Log("Target Disappeared");
        }

        if(other.CompareTag("Player") && player.flashLightState.On)
        {
            var enemyPlayer = other.GetComponent<PlayerBehavior>();
            if(!enemyPlayer.playerState.IsDead)
                enemyPlayer.Kill();
        }
}
}
