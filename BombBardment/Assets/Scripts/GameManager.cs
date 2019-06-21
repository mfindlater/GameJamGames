using Rewired;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public int player1Id = 0;
    public int player2Id = 1;
    public int player3Id = 2;
    public int player4Id = 3;


    private int redTeamScore = 0;
    private int blueTeamScore = 0;

    public int roundTime = 30;

    public Transform player1RedSpawn;
    public Transform player2RedSpawn;

    public Transform player1BlueSpawn;
    public Transform player2BlueSpawn;


    public PlayerController pr1, pr2, pb1, pb2;
    private PlayerController p1, p2, p3, p4;

    private bool redWonRound = false;
    private bool blueWonRound = false;
    private bool isTimeOver = false;

    public Action StartedGame;
    public Action EndedGame;
    public Action StartedRound;
    public Action EndedRound;

    public Timer timer;
    public SpawnBombManager spawnBombManager;
    public GameObject leftScore1, leftScore2, leftScore3, rightScore1, rightScore2, rightScore3;

    private bool roundStarted = false;

    public GameObject redWins, blueWins, redWinsRound, blueWinsRound;

    void StartGame()
    {
        redTeamScore = 0;
        blueTeamScore = 0;

     
    }

    void StartRound()
    {
        redWinsRound.SetActive(false);
        blueWinsRound.SetActive(false);
        redWonRound = false;
        blueWonRound = false;
        isTimeOver = false;
        timer.timeLeft = roundTime;
        timer.timeActive = true;
   
        


        

    //Spawn Players
        p1 = pr1.Spawn(player1RedSpawn.transform.position, Quaternion.Euler(0, 0, -90));
        p1.playerId = player1Id;
        p1.FacingLeft = false;

        p2 = pr2.Spawn(player2RedSpawn.transform.position, Quaternion.Euler(0, 0, -90));
        p2.playerId = player2Id;
        p2.FacingLeft = false;

        p3 = pb1.Spawn(player1BlueSpawn.transform.position, Quaternion.Euler(0, 0, 90));
        p3.playerId = player3Id;
        p3.FacingLeft = true;

        p4 = pb2.Spawn(player2BlueSpawn.transform.position, Quaternion.Euler(0, 0, 90));
        p4.playerId = player4Id;
        p4.FacingLeft = true;

        roundStarted = true;

        spawnBombManager.StartBombs();
    }

   

    void EndRound()
    {
        spawnBombManager.StopBombs();


        if (redWonRound)
        {
            redTeamScore += 1;
            redWinsRound.SetActive(true);
        }
        else if (blueWonRound)
        {
            blueTeamScore += 1;
            blueWinsRound.SetActive(true);
        }
        else if (isTimeOver)
        {
            int bluePoints = 0;
            int redPoints = 0;

            if (p1 && !p1.isAlive)
            {
                bluePoints += 1;
            }

            if (p2 && !p2.isAlive)
            {
                bluePoints += 1;
            }

            if (p3 && !p3.isAlive)
            {
                redPoints += 1;
            }

            if (p4 && !p4.isAlive)
            {
                redPoints += 1;
            }


            if(redPoints > bluePoints)
            {
                redTeamScore += 1;
            }

            if (bluePoints > redPoints)
            {
                blueTeamScore += 1;
            }
        }


        UpdateScoreUI();

        if (p1 && p1.isAlive)
        {
            p1.Recycle();
        }

        if (p2 && p2.isAlive)
        {
            p2.Recycle();
        }

        if (p3 && p3.isAlive)
        {
            p3.Recycle();
        }

        if (p4 && p4.isAlive)
        {
            p4.Recycle();
        }

        roundStarted = false;
        timer.timeActive = false;
    }

    void UpdateScoreUI()
    {
        if(redTeamScore >= 1)leftScore1.SetActive(true);
        if(redTeamScore >= 2) leftScore2.SetActive(true);
        if(redTeamScore >= 3) leftScore3.SetActive(true);

        if (blueTeamScore >= 1) rightScore1.SetActive(true);
        if (blueTeamScore >= 2) rightScore2.SetActive(true);
        if (blueTeamScore >= 3) rightScore3.SetActive(true);

    }

    void EndGame()
    {
        if(redTeamScore == 3)
        {
            redWins.SetActive(true);
        }
        else if(blueTeamScore == 3)
        {
            blueWins.SetActive(true);
        }


    }



    void Start () {
        StartCoroutine(RunGame());
         
    }
	
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            spawnBombManager.StopBombs();
            StopAllCoroutines();
            SceneManager.LoadScene("Title");
        }
		
	}

    IEnumerator RunGame()
    {
        StartGame();

        while(!IsGameOver())
        {
            yield return new WaitForSeconds(1.5f);
            StartRound();

            while(!IsRoundOver())
            {
                yield return null;
            }

            EndRound();
            
        }

        EndGame();

        yield return new WaitForSeconds(5);

        SceneManager.LoadScene("Title");
    }

    private bool IsRoundOver()
    {

        redWonRound = (!p3.isAlive && !p4.isAlive);

        blueWonRound = (!p1.isAlive && !p2.isAlive);

        isTimeOver = timer.timeLeft <= 0 && roundStarted;

        bool result = redWonRound || blueWonRound || isTimeOver;

        return result;
    }

    private bool IsGameOver()
    {
        return redTeamScore == 3 || blueTeamScore == 3;
    }
}
