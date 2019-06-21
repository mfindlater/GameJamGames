using UnityEngine;
using System.Collections;
using System;

public class Hitter : MonoBehaviour {
    public Robot parent { get; set; }
    private bool HitsLeftTorso { get { return parent.leftFacing; } }

    void OnTriggerEnter(Collider other)
    {
        var hit = other.gameObject.GetComponent<RobotHitTrigger>();
        if (hit != null)
        {
            if ((HitsLeftTorso && hit.tag == "LTorso")
                || (!HitsLeftTorso && hit.tag == "RTorso"))
            {
                throw new Exception(gameObject.tag + " hit " + hit.tag);
            }
        }
    }
}
