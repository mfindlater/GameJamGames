using UnityEngine;
using System.Collections;

public class BuildingTooHigh : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag.Equals("BuildingTop"))
        {
            Application.LoadLevel("GameOver");
        }
    }
}
