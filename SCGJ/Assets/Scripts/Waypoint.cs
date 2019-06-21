using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D c)
    {
        if(c.gameObject.tag.Equals("Enemy"))
        {

            c.gameObject.GetComponent<Enemy>().NextWaypoint();
			c.gameObject.GetComponent<Enemy>().seePlayer = false;
        }
    }
}
