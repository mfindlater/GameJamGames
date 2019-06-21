using UnityEngine;
using System.Collections;

public class OutOfBounds : MonoBehaviour {

    GameManager m_gameManager;

    void Awake()
    {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            collider.gameObject.SendMessage("Respawn");
            m_gameManager.PlayerFailed();
        }
    }
}
