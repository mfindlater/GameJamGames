using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    public Transform spawnPosition;
    private bool m_bDeactivate = false;

    public void Deactivate()
    {
        m_bDeactivate = true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Set Checkpoint");

        if (m_bDeactivate)
            return;

        if(collider.gameObject.tag.Equals("Player"))
        {
            Player player = collider.gameObject.GetComponent<Player>();
            player.SetCheckpoint(this);
        }
    }

}
