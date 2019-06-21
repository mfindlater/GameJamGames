using UnityEngine;
using System.Collections;

public class MoveButtonTrigger : MonoBehaviour {

    void Start()
    {
        //transform.position = new Vector3(transform.position.x, GetComponentInChildren<MoveButton>().upY, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CockpitPlayer>() != null)
        {
            GetComponentInChildren<MoveButton>().Press();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CockpitPlayer>() != null)
        {
            GetComponentInChildren<MoveButton>().Unpress();
        }
    }
}
