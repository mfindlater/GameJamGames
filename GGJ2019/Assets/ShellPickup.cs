using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellPickup : MonoBehaviour
{
    public string shellName = "Shell1";

    void OnTriggerEnter2D(Collider2D c)
    {
        if(c.tag.Equals("Player"))
        {
            Player player = c.GetComponent<Player>();

            if(!player.HasShell)
            {
                player.SetShell(shellName);

                Destroy(gameObject);
            }
        }
    }
}
