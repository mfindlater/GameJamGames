using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {
	bool move;
	// Use this for initialization
	void Start () {
		move = false;
		StartCoroutine(delayFunc());
	}
	
	// Update is called once per frame
	void Update () {
		if (move)
		{
			
		}
	}
	
	IEnumerator delayFunc() {
        yield return new WaitForSeconds(1f);
		transform.parent = null;
		startMovement();
	}
	
	void startMovement () {
		move = true;
	}
}