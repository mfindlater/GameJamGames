using UnityEngine;
using System.Collections;

public class CaptureZone : MonoBehaviour {

	public AudioClip teleportSound;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnDrawGizmos()
	{
		Gizmos.color = new Color (1, 0.3f, 0.3f, 0.4f);
		Gizmos.DrawCube(transform.position, new Vector3(transform.localScale.x, transform.localScale.y, 1));
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.GetComponent<Enemy>())
		{
			Enemy enemy = col.gameObject.GetComponent<Enemy>();
			if (enemy.Lassoed.isLassoed && !enemy.IsDead)
			{
				audio.clip = teleportSound;
				audio.Play();
				enemy.Lassoed.target.GetComponent<Player>().DestroyLasso();
				enemy.Captured();
			}
		}
	}
}
