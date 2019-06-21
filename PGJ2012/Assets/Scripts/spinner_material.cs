using UnityEngine;
using System.Collections;

public class spinner_material : MonoBehaviour {
	public Material material1;
    public Material material2;
	public float duration = 5.0F;
	// Use this for initialization
	void Start () {
		renderer.material = material1;
	}
	
	// Update is called once per frame
	void Update () {
		float lerp = Mathf.PingPong(Time.time, duration) / duration;
        renderer.material.Lerp(material1, material2, lerp);
	}
}
