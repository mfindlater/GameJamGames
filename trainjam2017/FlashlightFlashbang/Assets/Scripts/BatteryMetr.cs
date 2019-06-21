using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryMetr : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI(){
		GUI.Box(new Rect(5f, 10f, 5f, 5f), "Battery Meter");
	}
}
