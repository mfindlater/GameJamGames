/// <summary>
/// Name:Matt Bridges
/// Date: 11/8/14
/// Comment: Day/Night cycle timer for Philly Game Jam 2014 Project
/// </summary>


using UnityEngine;
using System;
using System.Collections;

public enum TimeOfDay
{
    DayTime,
    NightTime
}

public class DayNightCycler : MonoBehaviour {
	
	public GameObject backgroundTexture;				//Object reference for the background texture
	private bool sunUp = true;							//Track if sun is up or down
	public bool gameOver=false;							//Track game over state
	private float blendAmt=0;							//Holds blend modifier amount to control the shader

    public Action<TimeOfDay> TimeOfDayChanged;


	public float dayTime= .333f;
    public float nightTime = 5;
	public bool showText;
	public bool roundOver;
	public int gameTime=10;
	private int _gameTime;
    public float startGameWaitTime = 10;

    private GameObject player;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }
	
	// Use this for initialization
	IEnumerator Start () {
		backgroundTexture.renderer.material.SetFloat ("_Blend", blendAmt);
		_gameTime = gameTime;
		showText = true;
        roundOver = true;
        player.GetComponent<Platformer2DUserControl>().enabled = false;
        yield return new WaitForSeconds(startGameWaitTime);
        player.GetComponent<Platformer2DUserControl>().Transform();
        player.GetComponent<Platformer2DUserControl>().enabled = true;
        yield return StartCoroutine(SunDown());
        yield return StartCoroutine(DayNightCycle());
	}
	void Update()
	{
		//if (Input.anyKeyDown)
			//StartGame ();
	}
	//Sun down fader
	IEnumerator SunDown()
	{
		//Check to make sure sun is up, if so fade to sun down
		while(sunUp)
		{
			yield return new WaitForSeconds (.01f);
			blendAmt += .01f;
			backgroundTexture.renderer.material.SetFloat("_Blend", blendAmt);
		
			if(blendAmt>1)
			{
				blendAmt = 1;
				sunUp = false;

			}
		}
	}
	//Sun up fader
	IEnumerator SunUp()
	{
        
		//Check to make sure sun is down, if so fade to sun up
		while(!sunUp)
		{	
			yield return new WaitForSeconds (.01f);
			blendAmt -= .01f;

			if(blendAmt<0)
			{
				blendAmt = 0;
				sunUp = true;

			}
			backgroundTexture.renderer.material.SetFloat("_Blend", blendAmt);
		}
	}
	IEnumerator GameTimer()
	{
		while (!roundOver)
		{
			yield return new WaitForSeconds(1);

			_gameTime--;
			if(_gameTime == 0)
			{
				//Round Over Function
				RoundOver();

			}
		}

	}

    IEnumerator DayNightCycle()
    {
        //Check to make sure the game isnt over,if so cycle the time of day
        while (!gameOver)
        {
            
            yield return new WaitForSeconds(nightTime);
            OnTimeOfDayChanged(TimeOfDay.DayTime);
            yield return StartCoroutine("SunUp");
            yield return new WaitForSeconds(dayTime);
            OnTimeOfDayChanged(TimeOfDay.NightTime);
            yield return StartCoroutine("SunDown");
        }
    }

    private void OnTimeOfDayChanged(TimeOfDay tod) 
    {
        if (TimeOfDayChanged != null)
            TimeOfDayChanged(tod);
    }

	void OnGUI()
	{
		if(!showText)
			GUI.Label (new Rect(30,30,150,30),"Remaining Time: " + _gameTime);
	}
	void StartGame()
	{
		if(roundOver)
		{
			showText = false;
			roundOver = false;
			StartCoroutine ("SunDown");
			StartCoroutine ("GameTimer");
		}

	}
	void RoundOver()
	{
		roundOver = true;
		showText = true;
		StartCoroutine ("SunUp");
		_gameTime = 10;
	}
}