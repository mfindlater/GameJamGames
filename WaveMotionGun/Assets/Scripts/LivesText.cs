using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesText : MonoBehaviour {

    private Text text;
    private Player player;

	// Use this for initialization
	void Awake () {
        text = GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        text.text = player.lives.ToString();
		
	}
}
