using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	Health health;
	// Use this for initialization
	void Start () {
		health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
		health.OnDameged += ShakeCamera;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ShakeCamera()
	{
        iTween.ShakePosition(gameObject, iTween.Hash("name", "shake", "amount", new Vector3(5f, 5f, 0), "time", 0.5f));
		//iTween.ShakePosition(gameObject, new Vector3(5f, 5f, 0), 0.5f);
	}
}
