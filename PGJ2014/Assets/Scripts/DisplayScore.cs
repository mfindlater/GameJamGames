using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayScore : MonoBehaviour {



	// Use this for initialization
	void Start () {

        GetComponent<Text>().text = "FINAL SCORE: " + Score.instance.score.ToString("D7");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
