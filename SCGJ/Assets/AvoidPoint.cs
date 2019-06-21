using UnityEngine;
using System.Collections;

public class AvoidPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter2D(Collision2D c)
	{
		Debug.Log(c.gameObject.name);
		if(c.gameObject.tag.Equals("Enemy"))
		{
			if(c.gameObject.GetComponent<Enemy>().IsLassoed)
			{
				Debug.Log(c.gameObject.GetComponent<Enemy>().IsLassoed);
				Physics2D.IgnoreCollision(c.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
			} else
			{
				Physics2D.IgnoreCollision(c.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>(),false);
			}
			c.gameObject.GetComponent<Enemy>().NextWaypoint();
			c.gameObject.GetComponent<Enemy>().StopCoroutine("SeePlayer");
		}
	}
}
