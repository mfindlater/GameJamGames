using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour {
	public GameObject battery;
	public GameObject dead;
	public GameObject win;
	public GameObject hundred;
	public GameObject seventyfive;
	public GameObject fifty;
	public GameObject twentyfive;
	public GameObject ready;

	// Use this for initialization
	void OnEnable () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Killed (){
		battery.SetActive (false);
		dead.SetActive (true);
	}

	public void Win () {
		battery.SetActive (false);
		win.SetActive (true);
	}

	public void Ready () {
		ready.SetActive (false);
	}

	public void Battery (float batteryPercent) {
		if(batteryPercent == 100.0f){
			hundred.SetActive (true);
			seventyfive.SetActive (true);
			fifty.SetActive (true);
			twentyfive.SetActive (true);	
		} else if(batteryPercent <= 75.0f && batteryPercent > 50f){
			hundred.SetActive (false);
			seventyfive.SetActive (true);
			fifty.SetActive (true);
			twentyfive.SetActive (true);
		} else if(batteryPercent <= 50.0f && batteryPercent > 25f){
			hundred.SetActive (false);
			seventyfive.SetActive (false);
			fifty.SetActive (true);
			twentyfive.SetActive (true);
		} else if(batteryPercent <= 25.0f && batteryPercent > 0f){
			hundred.SetActive (false);
			seventyfive.SetActive (false);
			fifty.SetActive (false);
			twentyfive.SetActive (true);
		} else if(batteryPercent <= 0.0f){
			hundred.SetActive (false);
			seventyfive.SetActive (false);
			fifty.SetActive (false);
			twentyfive.SetActive (false);
		}
	}

	//this script is going to
	//should know whihc player you are
}
