using UnityEngine;
using System.Collections;

public class PunchButtonTrigger : MonoBehaviour {

    int collisionCount = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CockpitPlayer>() != null)
        {
            collisionCount++;
            GetComponentInChildren<PunchButton>().Press();
        }
    }

    void OnTriggerExit(Collider other)
    {
        collisionCount--;
        if (other.GetComponent<CockpitPlayer>() != null)
        {
            GetComponentInChildren<PunchButton>().Unpress();
        }
    }

    public bool HasColliders()
    {
        return collisionCount > 0;
    }
}
