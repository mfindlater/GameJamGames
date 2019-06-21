using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyKillListener
{
    void OnKill(Enemy e);
}

public class EnemySpawnManager : MonoBehaviour, IEnemyKillListener {

    public Enemy enemyPrefab;
    public float min = 2.5f;
    public float max = 2.5f;
    public bool spawnEnemies = true;

    public float minScreenY = 0;
    public float maxScreenY = 100;
    public float offscreenX = 100;

    public static Dictionary<EnemyType, int> enemyCount;

    List<Enemy> enemies = new List<Enemy>();

    public void KillAll()
    {
        for(int i=0; i < enemies.Count; i++)
        {
            if(enemies[i].alive)
                enemies[i].Kill(false);
        }
        enemies.Clear();
    }

    IEnumerator SpawnEnemies()
    {
        while(spawnEnemies)
        {
            float t = Random.Range(min, min);
            yield return new WaitForSeconds(t);

            int waveType = Random.Range(0, 6) + 1;

            switch(waveType)
            {
                case 1:
                    yield return SpawnWave01();
                    break;
                case 2:
                    yield return SpawnWave02();
                    break;
                case 3:
                    yield return SpawnWave03();
                    break;
                case 4:
                    yield return SpawnWave04();
                    break;
                case 5:
                    yield return SpawnWave05();
                    break;
                case 6:
                    yield return SpawnWave06();
                    break;
            }
        }
        yield return null;
    }

    void CountEnemy(Enemy e)
    {
        enemyCount[e.enemyType]++;
    }

    void SpawnEnemy(EnemyType t, float y, float speed = 5)
    {
        var e = enemyPrefab.Spawn();
        e.killListener = this;
        e.SetType(t);
        e.transform.position = new Vector3(offscreenX, y);
        CountEnemy(e);
        enemies.Add(e);
    }

    IEnumerator SpawnWave01()
    {
        SpawnEnemy(EnemyType.Kamikaze, 5);
        SpawnEnemy(EnemyType.Kamikaze, -5);
        yield return null;
    }

    IEnumerator SpawnWave02()
    {
        SpawnEnemy(EnemyType.StopAndShoot, 3.5f);
        yield return new WaitForSeconds(1.5f);
        SpawnEnemy(EnemyType.StopAndShoot, 0);
        yield return new WaitForSeconds(1.5f);
        SpawnEnemy(EnemyType.StopAndShoot, -3.5f);
    }

    IEnumerator SpawnWave03()
    {
        SpawnEnemy(EnemyType.Straightforward, 3.5f, 8);
        SpawnEnemy(EnemyType.Straightforward, 0, 8);
        SpawnEnemy(EnemyType.Straightforward, -3.5f, 8);
        yield return new WaitForSeconds(1.5f);
    }

    IEnumerator SpawnWave04()
    {
        SpawnEnemy(EnemyType.StopAndShoot, 3.5f, 10);
        yield return new WaitForSeconds(1.5f);
        SpawnEnemy(EnemyType.StopAndShoot, 0, 8);
        yield return new WaitForSeconds(1.5f);
        SpawnEnemy(EnemyType.StopAndShoot, -3.5f, 6);
    }

    IEnumerator SpawnWave05()
    {
        SpawnEnemy(EnemyType.Kamikaze, 3.5f);
        yield return new WaitForSeconds(1.5f);
        SpawnEnemy(EnemyType.Straightforward, 0);
        yield return new WaitForSeconds(1.5f);
        SpawnEnemy(EnemyType.StopAndShoot, -3.5f);
    }

    IEnumerator SpawnWave06()
    {
        SpawnEnemy(EnemyType.Wildcard, 0, 3);
        yield return new WaitForSeconds(2f);
        SpawnEnemy(EnemyType.Wildcard, 0, 3);
        yield return new WaitForSeconds(2f);
        SpawnEnemy(EnemyType.Wildcard, 0, 3);
    }

    // Use this for initialization
    void Start ()
    {
        enemyPrefab.CreatePool(50);

        enemyCount = new Dictionary<EnemyType, int>();
        enemyCount.Add(EnemyType.Kamikaze, 0);
        enemyCount.Add(EnemyType.StopAndShoot, 0);
        enemyCount.Add(EnemyType.Straightforward, 0);
        enemyCount.Add(EnemyType.Wildcard, 0);

        StartCoroutine(SpawnEnemies());
	}

    public void OnKill(Enemy e)
    {
        enemyCount[e.enemyType]--;
    }
}
