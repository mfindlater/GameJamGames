using UnityEngine;
using System.Collections;

public class Fist : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.tag = "Fist";
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision c)
    {
        c.gameObject.SendMessageUpwards("TakeDamage", Def.Instance.punchDamage);
        Sounds.Instance.PlayHit();
    }
}
