using UnityEngine;
using System.Collections;

public class Sounds : Singleton<Sounds> {
    public AudioClip jump;
    public AudioClip buttonPress;
    public AudioClip[] hits;
    
    private AudioSource source;

    void Start(){
        source = Instance.GetComponent<AudioSource>().audio;
    }

    public void PlayHit()
    {
        AudioClip sound = Instance.hits[Random.Range(0, Instance.hits.Length)];

        source.clip = sound;
        source.Play();
    }

    public void PlayJump()
    {
        source.clip = jump;
        source.Play();
    }

    public void PlayButtonPress()
    {
        source.clip = buttonPress;
        source.Play();
    }
}
