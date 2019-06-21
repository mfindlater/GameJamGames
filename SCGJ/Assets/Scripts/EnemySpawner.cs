using UnityEngine;
using System;
using System.Collections;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour {

    public bool IsRunning = true;
    public float MinTimeBetweenSpawn = 3f;
    public float MaxTimeBetweenSpawn = 10f;
    public GameObject BountyEnemy;
    public GameObject[] EnemyTypes;
    public Transform[] SpawnPoints;

    private int enemyCount;
    public int MaxEnemies = 6;
    public Action EnemiesCleared;

    private bool NeedBountyEnemy = true;

	// Use this for initialization
	void Start () {
        StartCoroutine(Run());
	}

    IEnumerator Run()
    {
        while(IsRunning)
        {
            if (enemyCount < MaxEnemies)
            {
                SpawnEnemy();
            }

            float time = Random.Range(MinTimeBetweenSpawn, MaxTimeBetweenSpawn);
            yield return new WaitForSeconds(time);
        }
    }

    void OnEnemyIncapacited()
    {
        enemyCount--;
        if(enemyCount == 0)
        {
            if (EnemiesCleared != null)
                EnemiesCleared();
        }
    }

    void SpawnEnemy()
    {

        Transform spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)];

        if (NeedBountyEnemy)
        {
            var enemy = (GameObject)GameObject.Instantiate(BountyEnemy);
            enemy.GetComponent<Enemy>().Incapacitated += BountyCaptured;
            enemy.transform.position = spawnPoint.transform.position;
            NeedBountyEnemy = false;
        }
        else
        {
            GameObject g = EnemyTypes[Random.Range(0, EnemyTypes.Length)];
            var enemy = (GameObject)GameObject.Instantiate(g);
            enemy.GetComponent<Enemy>().Incapacitated += OnEnemyIncapacited;
            enemy.transform.position = spawnPoint.transform.position;
        }
        
          enemyCount++;
    }

    private void BountyCaptured()
    {
        NeedBountyEnemy = true;
        OnEnemyIncapacited();
    }

    
}
