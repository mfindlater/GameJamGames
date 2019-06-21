using UnityEngine;
using System.Collections;

public class item_rotate : MonoBehaviour {
	public float rotSpeed = 4.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float rotY = transform.eulerAngles.y + ((rotSpeed*10) * Time.deltaTime);
		transform.eulerAngles = new Vector3(0, rotY, 0);
		
		float posY = 0.2f + 0.2f*(Mathf.Sin(Time.time));
		transform.localPosition = new Vector3(transform.localPosition.x, posY, transform.localPosition.z);
	}
}
