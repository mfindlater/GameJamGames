using UnityEngine;
using System.Collections;

public class ball_force : MonoBehaviour {
	
	public GameObject spinner;
	float spinVal;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		spinVal = spinner.GetComponent<spinnerControl>().angularVelocity;
		Vector3 forceVector = Vector3.Normalize(transform.position);
		rigidbody.AddForce(forceVector * (spinVal));
	}
}
