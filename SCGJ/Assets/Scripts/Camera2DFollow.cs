using UnityEngine;
using System.Collections;

public class Camera2DFollow : MonoBehaviour
{

    public Transform target;
    public float dampTime = 0.15f;
    public Vector3 velocity;
    public Vector3 Offset = Vector3.zero;

    void Update()
    {

        if (target)
        {
            Vector3 point = camera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = (transform.position + Offset) + delta;
            transform.position =  Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }
}

