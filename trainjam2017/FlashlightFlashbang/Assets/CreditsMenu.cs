using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenu : MonoBehaviour {

	public Light creditslight;
	public Light wayneslight;
	public Light mattslight;
	public Light dylanslight;
	public Light marthaslight;
	public AudioSource click;
	public int timer;

	void OnWake () {
		timer = 0;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (timer < 78) {
			timer++;
			if (timer == 20) { 
				click.Play ();
				creditslight.gameObject.SetActive(true);
			} else if (timer == 40) {
				click.Play ();
				wayneslight.gameObject.SetActive(true);
			} else if (timer == 55) {
				click.Play ();
				mattslight.gameObject.SetActive(true);
			} else if (timer == 66) {
				click.Play ();
				dylanslight.gameObject.SetActive(true);
			} else if (timer == 77) {
				click.Play ();
				marthaslight.gameObject.SetActive(true);
			}
		}

	}
}
