using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public float time = 0.5f;
    List<EnemyType> enemiesKilled = new List<EnemyType>();

    IEnumerator Die()
    {
        yield return new WaitForSeconds(time);

        if(enemiesKilled.Count == 1)
        {
            ScoreManager.AddScore(enemiesKilled[0]);
        }

        if(enemiesKilled.Count > 1)
        {
            ScoreManager.AddScore(enemiesKilled);
        }

        enemiesKilled.Clear();

        transform.position = new Vector3(200, 200);
        gameObject.Recycle();
    }

    void OnEnable()
    {
        StartCoroutine(Die());
    }

	void OnTriggerEnter2D(Collider2D c)
    {
        if(c.tag.Equals("Enemy"))
        {
            Enemy e = c.GetComponent<Enemy>();
            e.SendMessage("Kill",true);
            enemiesKilled.Add(e.enemyType);

            if (enemiesKilled.Count > 1)
            {
                ScoreManager.SetTally(enemiesKilled.Count);
            }
        }
    }
}
