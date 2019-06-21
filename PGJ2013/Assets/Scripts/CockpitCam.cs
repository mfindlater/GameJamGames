using UnityEngine;
using System.Collections;

public class CockpitCam : MonoBehaviour {
    public float fractionOfScreenHeight;
    public bool left;

	// Use this for initialization
	void Start () {
        var cam = GetComponent<Camera>();
        float x = 0;
        if (!left)
        {
            x = Screen.width / 2;
        }
        cam.pixelRect = new Rect(x, 0, Screen.width / 2, Screen.height * fractionOfScreenHeight);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
