using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform m_transform;
    private Vector3 direction = new Vector2(-1,0);
    private float speed = 2f;

    float xMin = -12;
    float yMin = -12;
    float yMax = 12;

	// Use this for initialization
	void Awake () {
        m_transform = transform;
	}

    public void Set(Vector3 position, Vector3 direction, float speed)
    {
        m_transform.position = position;
        this.direction = direction;
        this.speed = speed;
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_transform.position += direction * speed * Time.deltaTime;

        if(m_transform.position.x < xMin || m_transform.position.y > yMax || m_transform.position.y < yMin)
        {
            Kill(false);
        }
    }

    public void Kill(bool givePoints)
    {
        gameObject.Recycle();
    }
}
