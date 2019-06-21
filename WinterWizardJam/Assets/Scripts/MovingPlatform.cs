using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

    public bool vertical = false;

    public Vector3 startingPoint;
    public Vector3 endingPoint;

    private ConstantForce2D m_constantForce2D;

    private bool wait = false;

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(startingPoint, 1);
        Gizmos.DrawSphere(endingPoint, 1);
        Gizmos.DrawLine(startingPoint, endingPoint);
    }

	void Awake () {
        m_constantForce2D = GetComponent<ConstantForce2D>();
	}

    void Update()
    {

        if(!wait)

        if (!vertical)
        {

            if (transform.position.x > endingPoint.x || transform.position.x < startingPoint.x)
            {
                m_constantForce2D.force = new Vector2(-m_constantForce2D.force.x, 0);
                StartCoroutine(Wait());
            }
        }
        else
        {
            if (transform.position.y > endingPoint.y || transform.position.y < startingPoint.y)
            {
                m_constantForce2D.force = new Vector2(m_constantForce2D.force.x, 0);
                StartCoroutine(Wait());
            }
        }


    }


    IEnumerator Wait()
    {
        wait = true;
        yield return new WaitForSeconds(1);
        wait = false;
    }


    
	
}
