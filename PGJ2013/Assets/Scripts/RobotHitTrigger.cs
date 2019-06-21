using UnityEngine;
using System.Collections;

public class RobotHitTrigger : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Fist")
        {
        }
    }

}
