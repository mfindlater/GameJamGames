using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Start,
    Playing,
    GameOver
}

public class GameManager : MonoBehaviour {

    private GameState currentState = GameState.Start;
    Player player;

	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        StartCoroutine(RunGame());
	}

    private void StartGame()
    {
        ScoreManager.Initialize();
    }

    private void EndGame()
    {

    }

    IEnumerator RunGame()
    {
        StartGame();

        while(currentState != GameState.GameOver)
        {
            if(player.lives <= 0)
            {
                currentState = GameState.GameOver;
                yield return new WaitForSeconds(2f);
                SceneManager.LoadScene("GameOver");
            }

            yield return null;
        }

        EndGame();
    }
}
