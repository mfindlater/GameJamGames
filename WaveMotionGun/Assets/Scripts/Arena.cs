using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour {

    public Rect bounds;

	void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, bounds.size);
    }
}
