using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	
	public float Speed = 400;
	// Use this for initialization
	void Start () {
		
		//transform.rigidbody.constraints = RigidbodyConstraints.None;
	
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.Rotate(Vector3.up, Time.deltaTime * Speed);
	}
}
