using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour {
	public Light flashbangLight;
	public Light startLight;
	public Light exitLight;
	public AudioSource click;
	public int timer;

	void OnWake () {
		timer = 0;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (timer < 56) {
			timer++;
			if (timer == 20) {
				click.Play ();
				flashbangLight.gameObject.SetActive(true);
			} else if (timer == 40) {
				click.Play ();
				startLight.gameObject.SetActive(true);
			} else if (timer == 55) {
				click.Play ();
				exitLight.gameObject.SetActive(true);
			}
		}
		
	}
}
