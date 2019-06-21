using UnityEngine;
using System.Collections;

public class RobotTorso : MonoBehaviour {

    Sprite sprite;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<Sprite>();
        sprite.Animations.Add("Idle", new int[] { 0 });
        sprite.Animations.Add("Right", new int[] { 0,1,2,3});
        sprite.Animations.Add("Left", new int[] { 3, 2, 1, 0});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
