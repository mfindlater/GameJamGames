using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    private Text text;

	// Use this for initialization
	void Awake ()
    {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        text.text = string.Format("{0:D8}", ScoreManager.Score);
	}
}
