using System.Collections;
using UnityEngine;

//SpawnBomb
public class SpawnBomb : MonoBehaviour {

    public float minTimerVariance = 2;
    public float maxTimerVariance = 4;
    public bool initializeStartupTime = true;
    public Bomb bombPrefab;

    private bool isSpawningBombs = false;
    private Bomb currentBomb;

    private int numPlayersNear = 0;

    private string playerTag = "Player";

    IEnumerator SpawnBombs()
    {
        if(initializeStartupTime)
            yield return new WaitForSeconds(Random.Range(minTimerVariance, maxTimerVariance));

        while (isSpawningBombs)
        {
            currentBomb = bombPrefab.Spawn(transform.position);
            while(currentBomb.InPlay || numPlayersNear > 0)
            {
                yield return null;
            }
            yield return new WaitForSeconds(Random.Range(minTimerVariance, maxTimerVariance));
        }
    }

	public void StartSpawningBombs ()
    {
        isSpawningBombs = true;
        StartCoroutine("SpawnBombs");
	}

    public void StopSpawningBombs()
    {
        isSpawningBombs = false;
        StopCoroutine("SpawnBombs");

        if(currentBomb != null && currentBomb.InPlay)
        {
            currentBomb.Recycle();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag.Equals(playerTag))
        {
            numPlayersNear += 1;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag.Equals(playerTag))
        {
            numPlayersNear -=1;
        }
    }


}
