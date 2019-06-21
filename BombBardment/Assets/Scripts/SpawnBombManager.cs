using UnityEngine;

//SpawnBombManager
public class SpawnBombManager : MonoBehaviour {
    private SpawnBomb[] blueSpawnPoints;
    private SpawnBomb[] redSpawnPoints;

	void Start () {
        var red = GameObject.FindGameObjectsWithTag("RedBombSpawn");
        var blue = GameObject.FindGameObjectsWithTag("BlueBombSpawn");

        redSpawnPoints = new SpawnBomb[red.Length];
        blueSpawnPoints = new SpawnBomb[blue.Length];

        for(int i=0; i < redSpawnPoints.Length; i++)
        {
            redSpawnPoints[i] = red[i].GetComponent<SpawnBomb>();
        }

        for (int i = 0; i < blueSpawnPoints.Length; i++)
        {
            blueSpawnPoints[i] = blue[i].GetComponent<SpawnBomb>();
        }
    }

    public void StartBombs()
    {
        for(int i=0; i < blueSpawnPoints.Length; i++)
        {
            blueSpawnPoints[i].StartSpawningBombs();
        }

        for (int i = 0; i < redSpawnPoints.Length; i++)
        {
            redSpawnPoints[i].StartSpawningBombs();
        }
    }

    public void StopBombs()
    {
        for (int i = 0; i < blueSpawnPoints.Length; i++)
        {
            blueSpawnPoints[i].StopSpawningBombs();
        }

        for (int i = 0; i < redSpawnPoints.Length; i++)
        {
            redSpawnPoints[i].StopSpawningBombs();
        }
    }
}
