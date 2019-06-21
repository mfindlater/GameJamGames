using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	
	
	private AudioClip thisAudio;

    void Start()
    {
        StartCoroutine(CleanUp());
    }

	void Awake()
	{
		thisAudio = GetComponent<AudioSource>().clip;
		AudioSource.PlayClipAtPoint(thisAudio, new Vector3(0, 0, 0),GetComponent<AudioSource>().volume);
	}

    protected GameObject _instigator;

    public float Lifetime = 1;

    public GameObject Instigator
    {
        get { return _instigator; }
        set { _instigator = value; }
    }

    IEnumerator CleanUp()
    {
        yield return new WaitForSeconds(Lifetime);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (!c.gameObject.tag.Equals("Enemy") && !c.gameObject.tag.Equals("Player"))
        {
            Destroy(gameObject);
        }
    }
}
