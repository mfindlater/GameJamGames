using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {
	
	
	public GameObject[] Items = new GameObject[10];
	public float[] Probs = new float[10];
	public float SecondRange = 5;
	
	private float totalElapsed;
	private float nextSpawnTime;
	
	public GameObject[] SpawnPoints = new  GameObject[3];
	
	// Use this for initialization
	void Start () {	
		nextSpawnTime = totalElapsed + Random.value * SecondRange;
	}
	
	// Update is called once per frame
	void Update () {
		
		totalElapsed += Time.deltaTime;
		
		if(totalElapsed >= nextSpawnTime)
		{
			int itemIndex = Choose(Probs);
			SpawnItem(itemIndex);
			nextSpawnTime = totalElapsed + Random.value * SecondRange;
		}
	
	}
	
	
	void SpawnItem(int i)
	{
		GameObject item = (GameObject)GameObject.Instantiate(Items[i]);
	
		item.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		int idx = Random.Range(0, SpawnPoints.Length-1);
		item.transform.position = SpawnPoints[idx].transform.position;
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		for(int i =0; i < SpawnPoints.Length; i++)
		{
			Gizmos.DrawSphere(SpawnPoints[i].transform.position, 1f);
		}
	}
	
	public int Choose(float[] probs)
	{
		float total = 0;
		
		foreach (float element in probs)
		{
			total += element;
		}
		
		float randomPoint = Random.value * total;
		
		for(int i=0; i < probs.Length; i++)
		{
			if(randomPoint < probs[i])
				return i;
			else
				randomPoint -= probs[i];
		}
		
		return probs.Length - 1;
	}
}
