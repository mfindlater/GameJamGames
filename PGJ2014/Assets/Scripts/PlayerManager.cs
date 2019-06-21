using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    public GameObject player;
    public Transform respawnPoint;
    public float respawnTime;
    public bool playerIsDead;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerIsDead = false;
    }

    IEnumerator Die()
    {
        if (!playerIsDead)
        {
            playerIsDead = true;
            player.GetComponent<Platformer2DUserControl>().enabled = false;
            yield return new WaitForSeconds(respawnTime);
            player.transform.position = respawnPoint.transform.position;
            player.GetComponent<Platformer2DUserControl>().enabled = true;
            playerIsDead = false;
        }
    }
}
