using UnityEngine;
using System.Collections;

public class IcePathNode : MonoBehaviour {

    public delegate void IcePathEvent(IcePathNode icePathNode);

    public event IcePathEvent IcePathNodeDestroyed;

    public float lifetimeInSeconds;

    void OnEnable()
    {
        StartCoroutine(Die());
    }

    void OnDisable()
    {
        IcePathNodeDestroyed = null;
    }
	
    IEnumerator Die()
    {
        yield return new WaitForSeconds(lifetimeInSeconds);
        
        if(IcePathNodeDestroyed != null)
        {
            IcePathNodeDestroyed(this);
        }

        gameObject.Recycle();
    }
}
