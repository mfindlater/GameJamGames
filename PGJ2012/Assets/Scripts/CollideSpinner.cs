using UnityEngine;
using System.Collections;

public class CollideSpinner : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if(string.Equals(collision.gameObject.name, "spinner"))
		{
			this.transform.parent = collision.gameObject.transform;
			this.rigidbody.isKinematic = true;
		}
	}
}
