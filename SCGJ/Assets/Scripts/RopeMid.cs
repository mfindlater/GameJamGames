using UnityEngine;
using System.Collections;

public class RopeMid : MonoBehaviour {
	
	public float speed = 0; 
	public Vector3 _velocity;

	//Set Layer Mask to determine what destroys the ropes
	public LayerMask dontHurt = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.position += (_velocity * speed) * Time.deltaTime;
	}
	
	public void SetVelocity(Vector3 vel)
	{
		_velocity = vel;
	}	

	void OnCollisionEnter2D(Collision2D col)
	{
		_velocity = new Vector3(0,0,0);
		Debug.Log ("LAYER::  " + col.gameObject.layer);

		
		if (IsInLayerMask(col.gameObject,dontHurt)) {
			Debug.Log ("LAYER " + col.gameObject.layer + " is in layer mask");
			Debug.Log ("NAME::  " + col.gameObject.name);
			//_ropeSpawner a = GameObject.FindWithTag("Player").GetComponent<_ropeSpawner>();
			//a.destroyTheRope();
		}
	}
	
	private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
	{
		// Convert the object's layer to a bitfield for comparison
		int objLayerMask = (1 << obj.layer);
		if ((layerMask.value & objLayerMask) > 0)  // Extra round brackets required!
			return true;
		else
			return false;
	}
}
