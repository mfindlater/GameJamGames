using UnityEngine;
using System.Collections;

public class Lifetime : MonoBehaviour {
	
	public float Seconds = 5;

	// Use this for initialization
	void Start () {
		
		DestroyObject(gameObject, Seconds);
	
	}
	
	// Update is called once per frame
	void Update () {
		
	
	
	}
}
