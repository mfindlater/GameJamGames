using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSoundGenerator : MonoBehaviour {

    public AudioClip[] batSounds;

    private AudioSource m_audioSource;

    public float minTime = 3;
    public float maxTime = 10;

    IEnumerator PlaySounds()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            int soundIndex = Random.Range(0, batSounds.Length);
            m_audioSource.clip = batSounds[soundIndex];
            m_audioSource.Play();
        }
    }

	// Use this for initialization
	void Start () {
        m_audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlaySounds());
	}
}
