using System.Collections;
using UnityEngine;

//Explosion
public class Explosion : MonoBehaviour {

    public float lifetime = 0.5f;

    private AudioSource audioSource;

    private string playerTag = "Player";

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

	// Use this for initialization
	void OnEnable ()
    {
        audioSource.Play();
        StartCoroutine(Vanish());
	}

    IEnumerator Vanish()
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.Recycle();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag.Equals(playerTag))
        {
            collider.gameObject.SendMessage("Die");
        }
    }
}
