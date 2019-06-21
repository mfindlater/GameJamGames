using UnityEngine;
using System.Collections;

public class RopeEnd : MonoBehaviour {

	//Set Vars
	public float Speed = 10; 
	public Vector3 _velocity;
	protected GameObject _instigator;
	
	protected Vector3 startPos;
	protected Vector3 endPos;
	
	public bool hitObject = false;

	public GameObject enemyHooked;
	
	//Set Layer Mask to determine what destroys the ropes
	public LayerMask dontHurt = 0;
	public LayerMask canBeLassoed = 0;
	
	private Gravity gravity;

	private AudioClip thisAudio;
	
	public Gravity Gravity
	{
		get { return gravity; }
	}
	
	void Awake()
	{
		gravity = GetComponent<Gravity>();
		thisAudio = GetComponent<AudioSource>().clip;
		AudioSource.PlayClipAtPoint(thisAudio, new Vector3(0, 0, 0),GetComponent<AudioSource>().volume);
	}
	
	void Start()
	{
		
	}

	void FixedUpdate()
	{
		gravity.Apply(rigidbody2D);
	}
	
	public GameObject Instigator
	{
		get { return _instigator; }
		set { _instigator = value; }
	}
	
	void Update()
	{
		checkRay();
	}

	void ForcedUpdate()
	{
		gameObject.GetComponent<Gravity>().Apply(gameObject.rigidbody2D);
	}
	
	void checkRay()
	{
		/*startPos = transform.position + new Vector3(8.5f,0,0);
		endPos = transform.position + new Vector3(8.5f,0,0) + (_velocity * Speed) * Time.deltaTime;
		Debug.DrawLine (startPos, endPos, Color.cyan);
		hitObject = Physics2D.Linecast(startPos, endPos, 1 << LayerMask.NameToLayer("Enemy"));
		hitObject = Physics2D.Linecast(startPos, endPos, 1 << LayerMask.NameToLayer("Env2"));*/
		transform.position += (_velocity * Speed) * Time.deltaTime;
	}	
	
	public void SetVelocity(Vector3 vel)
	{
		_velocity = vel;
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
		_velocity = new Vector3(0,0,0);
		Debug.Log ("LAYER::  " + col.gameObject.layer);
		
		if (IsInLayerMask(col.gameObject,canBeLassoed) && !hitObject) {
			Debug.Log ("LAYER " + col.gameObject.layer + " is in layer mask");
			Debug.Log ("NAME::  " + col.gameObject.name);
			col.gameObject.GetComponent<Lassoed>().isLassoed = true;
			col.gameObject.GetComponent<Enemy>().StopCoroutine("SeePlayer");
			col.gameObject.GetComponent<Enemy>().seePlayer = false;
			col.gameObject.GetComponent<Enemy>().animator.SetBool("Lassoed",true);
			enemyHooked = col.gameObject;
			col.gameObject.GetComponent<HingeJoint2D>().connectedBody = gameObject.rigidbody2D;
			RopeSpawner a = GameObject.FindWithTag("Player").GetComponent<RopeSpawner>();
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
			a.fishCaught(enemyHooked);

			}
		
		if (IsInLayerMask(col.gameObject,dontHurt) && !hitObject) {
			Debug.Log ("LAYER " + col.gameObject.layer + " is in layer mask");
			Debug.Log ("NAME::  " + col.gameObject.name);
			RopeSpawner a = GameObject.FindWithTag("Player").GetComponent<RopeSpawner>();
			a.destroyTheRope();
		}
		hitObject = true;
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
	
	private void FlixY()
	{
		//facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.y *= -1;
		transform.localScale = theScale;
	}
	
	public void ReverseGravity()
	{
		gravity.Reverse = !gravity.Reverse;
		FlixY();
	}
}
