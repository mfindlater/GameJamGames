using UnityEngine;
using System.Collections;

public class spinnerControl : MonoBehaviour {
	
 	public float angularVelocity = 10.0f;
	float spinnerRotation;
	// Use this for initialization
	void Start () {
		spinnerRotation = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		
		spinnerRotation += angularVelocity;
        transform.eulerAngles = new Vector3(0, spinnerRotation, 0);
	}
}
