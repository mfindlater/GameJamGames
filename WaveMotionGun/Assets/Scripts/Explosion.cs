using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(AudioSource))]
public class Explosion : MonoBehaviour {

    private AudioSource m_audioSource;
    private ParticleSystem m_particleSystem;
    public AudioClip explosionSound;

	// Use this for initialization
	void Awake ()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_particleSystem = GetComponent<ParticleSystem>();
	}

    void OnEnable()
    {
        if(explosionSound != null)
        {
            m_audioSource.clip = explosionSound;
            m_audioSource.Play();
        }

        m_particleSystem.Play();

        StartCoroutine(Lifetime());

    }

    IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(1);
        gameObject.Recycle();
    }
	
}
