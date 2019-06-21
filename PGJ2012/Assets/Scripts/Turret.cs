using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {
	
	public GameObject Bullet;
	public float Speed = 40;
	public float Interval = 1;
	float elapsed;
	// Use this for initialization
	void Start () {
		
		transform.Rotate(Vector3.up,Random.value * 360);
	
	}
	
	// Update is called once per frame
	void Update () {
		
		elapsed += Time.deltaTime;
		
		if(elapsed >= Interval)
		{
			elapsed = 0;
			GameObject b = (GameObject)Instantiate(Bullet, transform.position, transform.rotation);
			b.transform.parent = null;
			b.rigidbody.velocity = transform.TransformDirection(Vector3.forward * Speed);
		}
	
	}
}
