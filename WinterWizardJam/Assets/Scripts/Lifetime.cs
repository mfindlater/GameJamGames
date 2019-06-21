using UnityEngine;
using System.Collections;

public class Lifetime : MonoBehaviour {

    public float seconds = 0.5f;

	// Use this for initialization
	void OnEnable () {
        StartCoroutine(Die());
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerator Die()
    {
        yield return new WaitForSeconds(seconds);
        gameObject.Recycle();
    }
}
