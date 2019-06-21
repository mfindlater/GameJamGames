using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trap : MonoBehaviour {

    public UnityEvent TrapTriggered;
    public string ColliderTag = "Player";

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(ColliderTag))
        {
            TrapTriggered.Invoke();
        }
    }
	
}
