using UnityEngine;
using System.Collections;

public class bulletSpawner : MonoBehaviour {
	public float timeBetweenSpawns = 1f;
	public GameObject bullet;
	float lastSpawn;
	float randRot;
	// Use this for initialization
	void Start () {
		lastSpawn = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > lastSpawn + timeBetweenSpawns)
		{
			spawnBullet();
		}
	}
	void spawnBullet() {
		randRot = Random.Range(0f,360f);
			transform.localRotation = Quaternion.Euler(0, randRot, 0);
			Vector3 posVector = new Vector3(0,0,-30);
			
			GameObject bulletInst = Instantiate(bullet,posVector,Quaternion.LookRotation(posVector)) as GameObject;
			bulletInst.transform.parent = transform;
			transform.rotation = Quaternion.Euler(0, randRot, 0);
			lastSpawn = Time.time;
	}
		
}