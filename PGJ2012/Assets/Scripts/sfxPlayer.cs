using UnityEngine;
using System.Collections;

public class sfxPlayer : MonoBehaviour {
	
	AudioSource audioSource;
	public AudioClip[] Sounds;
	
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		audioSource.loop = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Play(int i)
	{
		audioSource.audio.clip = Sounds[i];
		audioSource.Play();
	}
}
