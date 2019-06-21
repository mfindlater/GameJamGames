using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPanel : MonoBehaviour {

	public Light optionslight;
	public AudioSource click;
	public int timer;

	void OnWake () {
		timer = 0;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (timer < 21) {
			timer++;
			if (timer == 20) { 
				click.Play ();
				optionslight.gameObject.SetActive(true);
			} 
		}

	}
}