using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RopeSpawner : MonoBehaviour {

	public GameObject ropeEndObject;
	public GameObject ropeMidObject;
	public GameObject ropeMidObjectM;
	public GameObject ropeMidObjectH;
	public GameObject ropeStartObject;
	public GameObject currentPlayer;

	private GameObject ropeStartClone;
	private GameObject ropeMidClone;
	private GameObject ropeMidCloneM;
	private GameObject ropeMidCloneH;
	private List<GameObject> ropeMidCloneArray;
	private GameObject ropeEndClone;

	public float ropeRange = 100f;

	private bool ropeFired = false;
	public bool maxLengthReached = false;

	private Vector2 launchedDirection;
	private float storedSpeed;
	private bool fishOnTheLine;

	public float reelSpeed = 100f;

	private bool canBreak = false;
	public float snapPoint = 200f;
	private float snapPointX = 250f;

	private GameObject fish;

	private float storeSpringJoint = 100f;

	public GameObject ropeSnapParticle;

	public AudioClip snapClip;
	private bool canPlayAudio = true;

    public bool RopeFired
    {
        get { return ropeFired; }
    }

	void Awake () 
	{
		snapPointX = snapPoint+ropeRange/2f;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		/*
        if (Input.GetKeyDown(KeyCode.C))
		{
			if(!ropeFired)
			{
				ropeMidCloneArray = new List<GameObject>();
				spawnStartClone(ropeStartObject);
				spawnEndClone(ropeEndObject);
				ropeFired = true;
				maxLengthReached = false;
			} else
			{
				ropeEndClone.GetComponent<RopeEnd>().enemyHooked.GetComponent<Lassoed>().isLassoed = false;
				ropeEndClone.GetComponent<RopeEnd>().enemyHooked.GetComponent<Lassoed>().myHinge.enabled = false;
				ropeEndClone.GetComponent<RopeEnd>().enemyHooked.GetComponent<Lassoed>().myDistanceJoint.enabled = false;
				ropeEndClone.GetComponent<RopeEnd>().enemyHooked.GetComponent<Lassoed>().mySpring.enabled = false;
				destroyTheRope();
			}

		}
        */
		if(ropeFired)checkRay();
		if(fishOnTheLine)bringCloser();
	}

    public void FireRope(Vector2 direction)
    {
        if (!ropeFired)
		{
			canPlayAudio = true;
			ropeMidCloneArray = new List<GameObject>();
            spawnStartClone(ropeStartObject, direction);
            spawnEndClone(ropeEndObject, direction);
            ropeFired = true;
            maxLengthReached = false;
        }
        else
        {
            destroyTheRope();
        }
    }

	void bringCloser()
	{
		var step = reelSpeed * Time.deltaTime;
		Vector3 startPos = ropeStartClone.gameObject.transform.position;
		Vector3 endPos = ropeEndClone.gameObject.transform.position;
		if(ropeRange < pythaTheo(startPos.x, startPos.y, endPos.x, endPos.y))
		{
			//ropeEndClone.transform.position = Vector2.MoveTowards(ropeEndClone.transform.position, ropeStartClone.transform.position, step);
			ropeEndClone.GetComponent<RopeEnd>().enemyHooked.GetComponent<Lassoed>().myDistanceJoint.enabled = false;
			ropeEndClone.GetComponent<RopeEnd>().enemyHooked.GetComponent<Lassoed>().mySpring.enabled = true;
		} else
		{
			ropeEndClone.GetComponent<RopeEnd>().enemyHooked.GetComponent<Lassoed>().myDistanceJoint.enabled = true;
			ropeEndClone.GetComponent<RopeEnd>().enemyHooked.GetComponent<Lassoed>().mySpring.enabled = false;
		}
	}

	public Vector3 LerpByDistance(Vector3 A, Vector3 B, float x)		
	{		
		Vector3 P = x * Vector3.Normalize(B - A) + A;		
		return P;		
	}

	void checkRay()
	{

		ropeStartClone.gameObject.transform.position = currentPlayer.transform.FindChild("_ropeSpawn").position;
		
		//Stop Velocity of End Rope after it reach the end of the length
		RopeEnd a = ropeEndClone.gameObject.GetComponent<RopeEnd>();
		Vector3 startPos = ropeStartClone.gameObject.transform.position;
		Vector3 endPos = ropeEndClone.gameObject.transform.position + (a._velocity * a.Speed) * Time.deltaTime;

		if(ropeMidCloneArray.Count > 0)
		{
			for (int i = ropeMidCloneArray.Count; i > 0; i--)
			{
				Destroy(ropeMidCloneArray[i-1]);
				ropeMidCloneArray.RemoveAt(i-1);
			}
		}

		if(pythaTheo(startPos.x, startPos.y, endPos.x, endPos.y) >= snapPoint && canBreak)
		{
			for (int j = 0; j < pythaTheo(startPos.x, startPos.y, endPos.x, endPos.y)/10; j++)
			{
				var mid = (GameObject)GameObject.Instantiate(ropeMidObjectH);
				mid.transform.position = LerpByDistance(startPos, endPos, j*10);
				mid.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 360-Vector3.Angle(startPos,endPos)); 
				ropeMidCloneArray.Add(mid);
				
			}
		} else if(pythaTheo(startPos.x, startPos.y, endPos.x, endPos.y) >= ropeRange)
		{
			for (int j = 0; j < pythaTheo(startPos.x, startPos.y, endPos.x, endPos.y)/10; j++)
			{
				var mid = (GameObject)GameObject.Instantiate(ropeMidObjectM);
				mid.transform.position = LerpByDistance(startPos, endPos, j*10);
				mid.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 360-Vector3.Angle(startPos,endPos)); 
				ropeMidCloneArray.Add(mid);
				
			}
		} else 
		{
			for (int j = 0; j < pythaTheo(startPos.x, startPos.y, endPos.x, endPos.y)/10; j++)
			{
				var mid = (GameObject)GameObject.Instantiate(ropeMidObject);
				mid.transform.position = LerpByDistance(startPos, endPos, j*10);
				mid.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 360-Vector3.Angle(startPos,endPos)); 
				ropeMidCloneArray.Add(mid);
				
			}
		}



		if(pythaTheo(startPos.x, startPos.y, endPos.x, endPos.y) >= snapPointX && canBreak)
		{
			snapTheRope();
		}

		Debug.DrawLine (startPos, endPos, Color.red);
		//Debug.Log (pythaTheo(startPos.x, startPos.y, endPos.x, endPos.y));
		if(ropeRange < pythaTheo(startPos.x, startPos.y, endPos.x, endPos.y) && !maxLengthReached)
		{
			Debug.Log("End of Rope");
			a.SendMessage("SetVelocity", new Vector3(0,0,0));
			maxLengthReached = true;
			if(!a.hitObject)destroyTheRope();
		}
	}

	void snapTheRope()
	{
		Debug.Log("Snapping Lasso");
		storeSpringJoint = fish.GetComponent<SpringJoint2D>().distance;
		fish.GetComponent<SpringJoint2D>().distance = 20;
		StartCoroutine(snapAndDestroyRope(0.5f)); 
	}

	IEnumerator snapAndDestroyRope(float waitTime)
	{

		yield return new WaitForSeconds(waitTime);
		createAudioComponent();
		snappingNow();
		destroyTheRope();
		fish.GetComponent<SpringJoint2D>().distance = storeSpringJoint;
	}
	
	public float pythaTheo(float x1, float y1 ,float x2, float y2)
	{
		float product = Mathf.Sqrt(Mathf.Pow(x1-x2,2)+Mathf.Pow(y1-y2,2));
		return product;
	}

	void spawnEndClone(GameObject ropeEnd, Vector2 direction)
	{
		direction.Normalize();

		var b = (GameObject)GameObject.Instantiate(ropeEnd);
		Vector3 theScale = transform.localScale;
		transform.localScale = theScale;


		if(direction == Vector2.zero)
		{
			if (currentPlayer.GetComponent<Player>().FacingRight)
			{
				direction = new Vector2(1, 0);
				theScale.x = Mathf.Abs(theScale.x);
			}
			else
			{
				direction = new Vector2(-1, 0);
				theScale.x = Mathf.Abs(theScale.x) * -1;
			}
		}

		b.transform.localScale = theScale;
		
		b.SendMessage("SetVelocity", direction.AsVector3());
		b.transform.position = currentPlayer.transform.position;
		ropeEndClone = b;
		launchedDirection = direction;
	}

	void spawnStartClone(GameObject ropeStart, Vector2 direction)
	{
		direction.Normalize();
		if(direction == Vector2.zero)
		{
			if (currentPlayer.GetComponent<Player>().FacingRight)
			{
				direction = new Vector2(-1, 0);
			}
			else
			{
				direction = new Vector2(1, 0);
			}
		}
		
		var a = (GameObject)GameObject.Instantiate(ropeStart);
		a.transform.position = currentPlayer.transform.FindChild("_ropeSpawn").position;
		ropeStartClone = a;

	}

	public void snappingNow()
	{
		RopeEnd b = ropeEndClone.gameObject.GetComponent<RopeEnd>();
		Vector3 startPos = ropeStartClone.gameObject.transform.position;
		Vector3 endPos = ropeEndClone.gameObject.transform.position + (b._velocity * b.Speed) * Time.deltaTime;
		
		for (int j = 0; j < pythaTheo(startPos.x, startPos.y, endPos.x, endPos.y)/5; j++)
		{
			var mid = (GameObject)GameObject.Instantiate(ropeSnapParticle);
			mid.transform.position = LerpByDistance(startPos, endPos, j*5);
			mid.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 360-Vector3.Angle(startPos,endPos)); 
			mid.gameObject.GetComponent<RopeSnap>().moveX = (startPos.x - mid.transform.position.x)/50;
			mid.gameObject.GetComponent<RopeSnap>().moveY = (startPos.y - mid.transform.position.y)/50;
			//mid.gameObject.transform.position += (a._velocity * a.Speed) * Time.deltaTime;
		}
		for (int j = 0; j < pythaTheo(startPos.x, startPos.y, endPos.x, endPos.y)/5; j++)
		{
			var mid = (GameObject)GameObject.Instantiate(ropeSnapParticle);
			mid.transform.position = LerpByDistance(startPos, endPos, j*5);
			mid.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 360-Vector3.Angle(startPos,endPos)); 
			mid.gameObject.GetComponent<RopeSnap>().moveX = (startPos.x - endPos.x)/50;
			mid.gameObject.GetComponent<RopeSnap>().moveY = (startPos.y - endPos.y)/50;
			//mid.gameObject.transform.position += (a._velocity * a.Speed) * Time.deltaTime;
		}
	}

	public void destroyTheRope()
	{
		// to count the number of objects:
		int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
		
		// to get an array of references to all those objects:
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		Debug.Log (enemyCount);

		for(int a = 0; a < enemyCount; a++)
		{
			enemies[a].GetComponent<Lassoed>().isLassoed = false;
			enemies[a].GetComponent<Lassoed>().myHinge.enabled = false;
			enemies[a].GetComponent<Lassoed>().myDistanceJoint.enabled = false;
			enemies[a].GetComponent<Lassoed>().mySpring.enabled = false;
			enemies[a].GetComponent<Lassoed>().isLassoed = false;
			enemies[a].GetComponent<Enemy>().animator.SetBool("Lassoed",false);
		}



		Destroy (ropeStartClone);
		if(ropeMidCloneArray.Count > 0)
		{
			for (int i = ropeMidCloneArray.Count; i > 0; i--)
			{
				//ropeMidCloneArray[i-1].rigidbody2D.AddRelativeForce(new Vector2(1f,1f));
				Destroy(ropeMidCloneArray[i-1]);
				ropeMidCloneArray.RemoveAt(i-1);
			}
		}

		Destroy(GetComponent<AudioSource>());
		Destroy (ropeEndClone);
		ropeFired = false;
		fishOnTheLine = false;
		canBreak = false;

	}

	void createAudioComponent()
	{
		if(canPlayAudio)
		{
			AudioSource audioSource = gameObject.AddComponent<AudioSource>();
			audioSource.clip = snapClip;
			audioSource.volume = .2f;
			AudioSource.PlayClipAtPoint(audioSource.clip, new Vector3(0, 0, 0),.2f);
			canPlayAudio = false;
		}
	}

	public void fishCaught(GameObject enemy)
	{
		fish = enemy;
		fishOnTheLine = true;
		canBreak = fish.GetComponent<Enemy>().bountyTarget;
	}
}
