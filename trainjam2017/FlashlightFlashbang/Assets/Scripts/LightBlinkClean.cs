using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlinkClean : MonoBehaviour {

	public float duration = 1.0f;
	public Light lt;

	// Use this for initialization
	void Start () {
		lt = GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
		float phi = Time.time / duration * 2 * Mathf.PI;
		float amplitude = Mathf.Cos (phi) * 0.5f + 0.5f;
		lt.intensity = amplitude;
	}
}
