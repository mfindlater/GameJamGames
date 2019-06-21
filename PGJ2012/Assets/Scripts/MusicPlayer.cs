using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	
	AudioSource audioSource;
	public AudioClip[] Songs = new AudioClip[10];
	
	int preSong = 0;
	int curSong = 6;
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		audioSource.loop = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!audio.isPlaying){
			if (curSong <8) {
        		audioSource.audio.clip = Songs[curSong+1];
        		audio.Play();
				curSong += 1;
			}
			else {
				audioSource.audio.clip = Songs[0];
        		audio.Play();
				curSong += 0;
			}
		}
	}
	
	public void Play(int i)
	{
		preSong = curSong;
		audioSource.audio.clip = Songs[i];
		audioSource.Play();
		curSong = i;
	}
	
	public void PlayPrev()
	{
		Play(preSong);
	}
}
