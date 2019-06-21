using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class howtoscript : MonoBehaviour {

	public Light controlight;
	public Light batteryight;
	public Light shineight;
	public Light buttonLight;
	public AudioSource click;
	public int timer;

	void OnWake () {
		timer = 0;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (timer < 66) {
			timer++;
			if (timer == 20) {
				click.Play ();
				controlight.gameObject.SetActive(true);
			} else if (timer == 40) {
				click.Play ();
				batteryight.gameObject.SetActive(true);
			} else if (timer == 55) {
				click.Play ();
				shineight.gameObject.SetActive(true);
			} else if (timer == 66) {
				click.Play ();
				buttonLight.gameObject.SetActive(true);
			}
		}

	}
}
