using UnityEngine;
using System.Collections;

public class RopeSnap : MonoBehaviour {

	public float waitTime = 1f;

	public float moveX = 1f;
	public float moveY = 1f;

	// Use this for initialization
	void Start () {
		StartCoroutine(destroyThis());
	}

	IEnumerator destroyThis()
	{
		yield return new WaitForSeconds(waitTime);
		Destroy(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position += new Vector3(moveX,moveY,0f);
	}
}
