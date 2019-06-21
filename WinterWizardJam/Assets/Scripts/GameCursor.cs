using UnityEngine;
using System.Collections;

public class GameCursor : MonoBehaviour {

    public float distance = 10;

	// Use this for initialization
	void Awake () {
        //Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 position = ray.GetPoint(distance);
        transform.position = position;
	}
}
