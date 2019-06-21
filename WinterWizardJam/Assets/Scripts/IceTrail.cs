using UnityEngine;
using System.Collections;

public class IceTrail : MonoBehaviour {

    public static IceTrail activeIceTrail;

    public float lifetimeInSeconds;

    private bool m_bTrailActive = true;

    private Player m_player;

    void Awake()
    {
        m_player = GameObject.Find("Player").GetComponent<Player>();
    }

	void OnEnable () {
        if(activeIceTrail !=null && activeIceTrail.isActiveAndEnabled)
        {
            activeIceTrail.Recycle();
        }

        activeIceTrail = this;
        m_bTrailActive = true;
        StartCoroutine(Done());
	}
	
	void Update () {

        if (m_bTrailActive)
        {
            var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            transform.position = position;
        }

       if(!m_player.IsMouseButtonHeld || !m_player.HasMana)
        {
            m_bTrailActive = false;
        }

    }

    IEnumerator Done()
    {
        yield return new WaitForSeconds(lifetimeInSeconds);
        gameObject.Recycle();
    }
}
