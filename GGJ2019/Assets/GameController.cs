using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] shellPrefabs;
    public Transform[] enemySpawnPoints;
    public Transform[] shellSpawnPoints;

    public float spawnInterval = 5;

    public float initialShellTime = 3f;

    int nextSpawnPoint = 0;
    int nextShellSpawnPoint = 0;

    void Awake()
    {
       shellSpawnPoints = GameObject.FindGameObjectsWithTag("ShellSpawnPoint").Select(g => g.transform).ToArray();
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(initialShellTime);
            SpawnShell();

        while(true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

    public void SpawnShell()
    {
        int index = Random.Range(0, shellPrefabs.Length-1);
        var shellPrefab = shellPrefabs[index];

        if(nextShellSpawnPoint > shellSpawnPoints.Length-1)
        {
            nextShellSpawnPoint = 0;
        }

        index = nextShellSpawnPoint;
        var spawnPoint = shellSpawnPoints[index];

        var go = GameObject.Instantiate(shellPrefab);

        go.transform.position = spawnPoint.position;
    }

    public void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Length-1);

        var enemyPrefab = enemyPrefabs[index];

        if(nextSpawnPoint > enemySpawnPoints.Length-1)
        {
            nextSpawnPoint = 0;
        }

        index = nextSpawnPoint;         
        var spawnPoint = enemySpawnPoints[index];

        var go = GameObject.Instantiate(enemyPrefab);

        go.transform.position = spawnPoint.position;

        nextSpawnPoint++;
    }
}
