using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portrait : MonoBehaviour {

    public float lifetime = 3;

	// Use this for initialization
	void Start () {
		
	}

    void OnEnable()
    {
        StartCoroutine(Dissapear());
    }

    IEnumerator Dissapear()
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.Recycle();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
