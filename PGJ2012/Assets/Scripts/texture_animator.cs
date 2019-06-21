using UnityEngine;
using System.Collections;

public class texture_animator : MonoBehaviour {
	public Texture2D[] texArray;
	public float ChangeInterval = 0.01f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (texArray.Length !=0) {
			int index = (int)(Time.time / ChangeInterval);
			index = index % texArray.Length;
			gameObject.GetComponent<LineRenderer>().renderer.material.mainTexture = texArray[index];
		}
		
	}
}
