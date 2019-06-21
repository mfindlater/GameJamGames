using UnityEngine;
using System.Collections;

public class GravityZone : MonoBehaviour {

	public GravityState gravZoneType;
	public bool reversedGravity;

	public Material lowMat;
	public Material normalMat;
	public Material zeroMat;

	private Material currentMat;

	public float lowSpeed = -5f;
	public float normalSpeed = -10f;

	// Use this for initialization
	void Awake () {
		switch(gravZoneType)
		{
		case GravityState.Low:
			currentMat =lowMat;
			break;
		case GravityState.Normal:
			currentMat = normalMat;
			break;
		case GravityState.Zero:
			currentMat = zeroMat;
			break;
		default:
			break;
		}

		particleSystem.renderer.material = currentMat;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate () {
		float currentSpeed = lowSpeed;
		switch(gravZoneType)
		{
		case GravityState.Low:
			currentSpeed = lowSpeed;
			break;
		case GravityState.Normal:
			currentSpeed = normalSpeed;
			break;
		case GravityState.Zero:
			currentSpeed = 0f;
			break;
		default:
			break;
		}

		if(reversedGravity)currentSpeed *= -1f;
		
		ParticleSystem.Particle[] p = new ParticleSystem.Particle[particleSystem.particleCount+1];
		int l = particleSystem.GetParticles(p);
		
		int i = 0;
		while (i < l) {
			p[i].velocity = new Vector3(0, p[i].lifetime / p[i].startLifetime * currentSpeed, 0);
			i++;
		}
		
		particleSystem.SetParticles(p, l);    
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log (":::::: _" + col.gameObject.name + "_:::::: " );
		if(col.gameObject.GetComponent<Player>())
		{
			col.gameObject.GetComponent<Gravity>().GravityState = gravZoneType;
			if(reversedGravity)
			{
				if(!col.gameObject.GetComponent<Player>().Gravity.Reverse)
				{
					col.gameObject.GetComponent<Player>().ReverseGravity();
				}
			} else
			{
				if(col.gameObject.GetComponent<Player>().Gravity.Reverse)
				{
					col.gameObject.GetComponent<Player>().ReverseGravity();
				}
			}
		}
		if(col.gameObject.GetComponent<Enemy>())
		{
			col.gameObject.GetComponent<Gravity>().GravityState = gravZoneType;
			if(reversedGravity)
			{
				if(!col.gameObject.GetComponent<Enemy>().Gravity.Reverse)
				{
					col.gameObject.GetComponent<Enemy>().ReverseGravity();
				}
			} else
			{
				if(col.gameObject.GetComponent<Enemy>().Gravity.Reverse)
				{
					col.gameObject.GetComponent<Enemy>().ReverseGravity();
				}
			}
		}
		if(col.gameObject.GetComponent<RopeEnd>())
		{
			col.gameObject.GetComponent<Gravity>().GravityState = gravZoneType;
			if(reversedGravity)
			{
				if(!col.gameObject.GetComponent<RopeEnd>().Gravity.Reverse)
				{
					col.gameObject.GetComponent<RopeEnd>().ReverseGravity();
				}
			} else
			{
				if(col.gameObject.GetComponent<RopeEnd>().Gravity.Reverse)
				{
					col.gameObject.GetComponent<RopeEnd>().ReverseGravity();
				}
			}
		}

	}

    void OnTriggerExit2D(Collider2D c)
    {
        if(c.GetComponent<Player>())
        {
            c.GetComponent<Player>().Gravity.GravityState = GravityState.Normal;

            if(c.GetComponent<Player>().Gravity.Reverse)
            {
                c.GetComponent<Player>().ReverseGravity();
            }

        }

        if(c.GetComponent<Enemy>())
        {
            c.GetComponent<Enemy>().Gravity.GravityState = GravityState.Normal;

            if (c.GetComponent<Enemy>().Gravity.Reverse)
            {
                c.GetComponent<Enemy>().ReverseGravity();
            }
        }

        if(c.GetComponent<RopeEnd>())
        {
            c.GetComponent<RopeEnd>().Gravity.GravityState = GravityState.Normal;

            if (c.GetComponent<RopeEnd>().Gravity.Reverse)
            {
                c.GetComponent<RopeEnd>().ReverseGravity();
            }
        }
    }

	/*void OnTriggerStay2D(Collider2D col)
	{
		if(col.gameObject.GetComponent<Player>())
		{
			if(reversedGravity == col.gameObject.GetComponent<Player>().Gravity.Reverse)return;
			col.gameObject.GetComponent<Gravity>().GravityState = gravZoneType;
			if(reversedGravity)
			{
				if(!col.gameObject.GetComponent<Player>().Gravity.Reverse)
				{
					col.gameObject.GetComponent<Player>().ReverseGravity();
				}
			} else
			{
				if(col.gameObject.GetComponent<Player>().Gravity.Reverse)
				{
					col.gameObject.GetComponent<Player>().ReverseGravity();
				}
			}
		}
		if(col.gameObject.GetComponent<Enemy>())
		{
			if(reversedGravity == col.gameObject.GetComponent<Enemy>().Gravity.Reverse)return;
			col.gameObject.GetComponent<Gravity>().GravityState = gravZoneType;
			if(reversedGravity)
			{
				if(!col.gameObject.GetComponent<Enemy>().Gravity.Reverse)
				{
					col.gameObject.GetComponent<Enemy>().ReverseGravity();
				}
			} else
			{
				if(col.gameObject.GetComponent<Enemy>().Gravity.Reverse)
				{
					col.gameObject.GetComponent<Enemy>().ReverseGravity();
				}
			}
		}
		if(col.gameObject.GetComponent<ropeEndPoint>())
		{
			if(reversedGravity == col.gameObject.GetComponent<ropeEndPoint>().Gravity.Reverse)return;
			col.gameObject.GetComponent<Gravity>().GravityState = gravZoneType;
			if(reversedGravity)
			{
				if(!col.gameObject.GetComponent<ropeEndPoint>().Gravity.Reverse)
				{
					col.gameObject.GetComponent<ropeEndPoint>().ReverseGravity();
				}
			} else
			{
				if(col.gameObject.GetComponent<ropeEndPoint>().Gravity.Reverse)
				{
					col.gameObject.GetComponent<ropeEndPoint>().ReverseGravity();
				}
			}
		}
		Debug.Log (":::::: _" + col.gameObject.name + "_:::::: " );
	}*/

	void OnDrawGizmos() 
	{
		switch(gravZoneType)
		{
			case GravityState.Low:
				Gizmos.color = new Color (0, 1, 1, .25f);
			break;
			case GravityState.Normal:
				Gizmos.color = new Color (0, 0, 1, .25f);
			break;
			case GravityState.Zero:
				Gizmos.color = new Color (1, 1, 1, .25f);
			break;
			default:
			break;
		}

		Gizmos.DrawCube(transform.position, new Vector3(transform.localScale.x, transform.localScale.y, 1));
	}
}
