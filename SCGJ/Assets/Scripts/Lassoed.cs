using UnityEngine;
using System.Collections;
[RequireComponent(typeof(SpringJoint2D), typeof(HingeJoint2D), typeof(DistanceJoint2D))]
public class Lassoed : MonoBehaviour {

	public bool isLassoed = false;
	public HingeJoint2D myHinge;
	public DistanceJoint2D myDistanceJoint;
	public SpringJoint2D mySpring;

	public GameObject target;
	public float smoothTime = 0.3f;
	public float xOffset = 1.0f;
	public float yOffset = 1.0f;
	
	private GameObject thisGameObject;
	private Vector2 velocity;

	// Use this for initialization
	void Awake() {
		myHinge = gameObject.GetComponent<HingeJoint2D>();
		myDistanceJoint = gameObject.GetComponent<DistanceJoint2D>();
		mySpring = gameObject.GetComponent<SpringJoint2D>();
		thisGameObject = gameObject;
		target = GameObject.FindGameObjectWithTag("Player");
	}


	
	// Update is called once per frame
	void Update () {
		if(isLassoed)
		{
			myHinge.enabled = true;
			//myDistanceJoint.enabled = true;
			//mySpring.enabled = true;
		} else
		{
			myHinge.enabled = false;
			myDistanceJoint.enabled = false;
			mySpring.enabled = false;
		}
	}

	void LateUpdate()
	{
		if(isLassoed)
		{
			float modX = Mathf.Lerp(thisGameObject.transform.position.x, target.transform.position.x + xOffset, Time.deltaTime * smoothTime);
			float modY = Mathf.Lerp(thisGameObject.transform.position.y, target.transform.position.y + yOffset, Time.deltaTime * smoothTime);		

			thisGameObject.transform.position = new Vector3(modX,modY,0);
		}
	}



}
