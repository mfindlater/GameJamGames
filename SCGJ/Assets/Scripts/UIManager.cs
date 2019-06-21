using UnityEngine;
using System.Collections;
using System;
using InControl;

public class UIManager : MonoBehaviour {

	// Use this for initialization
	private bool pause = false;
	private bool unpause = false;
	private float unpauseStart;
	private float unpauseCooldown = 0.3f;
	public float baseWidth = 320;
	public float baseHeight = 180;
	private Vector2 UIScale;
	public Texture2D healthFrame;
	private Rect healthRect = new Rect(4,4,97,10);
	public Texture2D healthBar;
	public Texture2D bulletFull;
	public Texture2D bulletEmpty;
	public Texture2D dollarSign;
	public GUISkin scoreSkin;
	public Texture2D gravZero;
	public Texture2D gravLow;
	private Texture2D gravMeterLow;
	public Texture2D[] gravLowFrames;
	public float lowFPS = 5;
	public Texture2D gravNormal;
	private Texture2D gravMeterNormal;
	public Texture2D[] gravNormalFrames;
	public float normalFPS = 10;

    private Health playerHealth;
    private float gravRot;

    private int cursorPos = 0;
    private int cursorMax = 1;
    public AudioClip selectSound;
    public AudioClip acceptSound;
    public AudioClip claxon;
    public AudioClip gameoverSound;
    private bool claxon5;
    private bool claxon4;
    private bool claxon3;
    private bool claxon2;
    private bool claxon1;
    private bool claxon0;

    Score score;

    private InputDevice input;

	Player player;
	Gravity playerGrav;
	void Start () {
		claxon5 = false;
		claxon4 = false;
		claxon3 = false;
		claxon2 = false;
		claxon1 = false;
		claxon0 = false;
		cursorPos = 0;
		unpauseStart = Time.time;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		score = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Score>();
		playerGrav = player.GetComponent<Gravity>();
        playerHealth = player.GetComponent<Health>();
        GameWorld.Paused = false;
        input = InputManager.ActiveDevice;
	}

	void FixedUpdate()
	{
		if (gravLowFrames.Length > 0)
		{
			//set gravMeterLow
			int indexL = (int)(Time.time * lowFPS);
			indexL = indexL % gravLowFrames.Length;
			gravMeterLow = gravLowFrames[indexL];
			//set gravMeterNormal
			int indexN = (int)(Time.time * normalFPS);
			indexN = indexN % gravNormalFrames.Length;
			gravMeterNormal = gravNormalFrames[indexN];
		}
		
	}
	void Update() {
		pause = false;

       if(Input.GetKeyDown(KeyCode.Escape))
       {
           Application.LoadLevel("Menu");
       }

		if ((Input.GetKeyDown(KeyCode.P) || input.GetControl(InputControlType.Start).WasPressed) && GameWorld.Paused == false && unpause == false)
		{
			pause = true;
			GameWorld.Paused = true;
			Time.timeScale = 0.0f;
		}
		if (unpause == true)
		{
			if (Time.time > unpauseStart + unpauseCooldown)
			{
				unpause = false;
			}
		}

	}
	
	// Update is called once per frame
	void OnGUI () {
		if ((Input.GetKeyDown(KeyCode.Tab) || input.GetControl(InputControlType.Start).WasPressed && pause == false && GameWorld.Paused == true))
		{			
			unpause = true;
			GameWorld.Paused = false;
			Time.timeScale = 1.0f;
			unpauseStart = Time.time;

		}
		if (GameWorld.GameOver == true)
		{
			if ((Input.GetKeyDown(KeyCode.DownArrow) || input.DPadDown.WasPressed || input.LeftStick.Down.WasPressed) && cursorPos < cursorMax)
			{
				cursorPos += 1;
				audio.clip = selectSound;
				audio.Play();
			}
			if ((Input.GetKeyDown(KeyCode.UpArrow) || input.DPadUp.WasPressed || input.LeftStick.Up.WasPressed) && cursorPos > 0)
			{
				cursorPos -= 1;
				audio.clip = selectSound;
				audio.Play();
			}
			if ((Input.GetKeyDown(KeyCode.Return) || input.Action1.WasPressed))
			{
				Accept();
				audio.clip = acceptSound;
				audio.Play();
			}
		}

		UIScale = new Vector2(Screen.width / baseWidth, Screen.height / baseHeight);
		GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (UIScale.x, UIScale.y, 1));
		if (!GameWorld.Paused && !GameWorld.GameOver)
		{
			int healthWidth;
			if (playerHealth.CurrentHealth > 0)
        	    healthWidth = 6 + ((int)(90 * (playerHealth.CurrentHealth / playerHealth.MaxHealth) - (int)(90 * (playerHealth.CurrentHealth / playerHealth.MaxHealth)) % 6));
			else
				healthWidth = 0;
			healthRect = new Rect(4,4,healthWidth,10);
			GUILayout.BeginArea (healthRect);
				GUI.DrawTexture(new Rect(0,0,97,10), healthBar);
			GUILayout.EndArea ();
			GUI.DrawTexture(new Rect(4,4,97,10), healthFrame);
			if (player.BulletCount > 0)
				GUI.DrawTexture(new Rect(8,16,13,6), bulletFull);
			if (player.BulletCount > 1)
				GUI.DrawTexture(new Rect(23,16,13,6), bulletFull);
			if (player.BulletCount > 2)
				GUI.DrawTexture(new Rect(38,16,13,6), bulletFull);
			if (player.BulletCount > 3)
				GUI.DrawTexture(new Rect(53,16,13,6), bulletFull);
			if (player.BulletCount > 4)
				GUI.DrawTexture(new Rect(68,16,13,6), bulletFull);
			if (player.BulletCount > 5)
				GUI.DrawTexture(new Rect(83,16,13,6), bulletFull);

			//Gravity Meter

			gravRot = Vector2.Angle(new Vector2(0,-1),playerGrav.Current);
			switch (playerGrav.GravityState)	
			{
				case GravityState.Zero:
					GUI.DrawTexture(new Rect(4,124,53,53), gravZero);
				break;
				case GravityState.Low:
					GUI.DrawTexture(new Rect(4,124,53,53), gravLow);
					if (gravLowFrames.Length > 0)
					{
						GUIUtility.RotateAroundPivot(gravRot, new Vector2(30*UIScale.x, 150*UIScale.y));
							GUI.DrawTexture(new Rect(-2,116,64,64), gravMeterLow);
						GUIUtility.RotateAroundPivot(-gravRot, new Vector2(30*UIScale.x, 150*UIScale.y));
					}
				break;
				case GravityState.Normal:
					GUI.DrawTexture(new Rect(4,124,53,53), gravNormal);
					if (gravNormalFrames.Length > 0)
					{
						GUIUtility.RotateAroundPivot(gravRot, new Vector2(30*UIScale.x, 150*UIScale.y));
							GUI.DrawTexture(new Rect(-2,116,64,64), gravMeterNormal);
						GUIUtility.RotateAroundPivot(-gravRot, new Vector2(30*UIScale.x, 150*UIScale.y));
					}
				break;
				default:	
				break;
			}

			//Score
			GUI.skin = scoreSkin;
			GUI.DrawTexture(new Rect(8,26,8,9), dollarSign);
			GUI.Label(new Rect(8,21,100,18), score.CurrentScore.ToString("D7"));
			//Time
			GUI.Label(new Rect(120,4,80,18), (Math.Truncate(score.TimeRemaining)).ToString());
			if (claxon5 == false && Math.Truncate(score.TimeRemaining) == 4)
			{	
				audio.volume = 0.6f;
				audio.pitch = 0.4f;
				audio.clip = claxon;
				audio.Play();
				claxon5 = true;
			}
			if (claxon4 == false && Math.Truncate(score.TimeRemaining) == 3)
			{
				audio.pitch = 0.4f;
				audio.clip = claxon;
				audio.Play();
				claxon4 = true;
			}
			if (claxon3 == false && Math.Truncate(score.TimeRemaining) == 2)
			{
				audio.pitch = 0.4f;
				audio.clip = claxon;
				audio.Play();
				claxon3 = true;
			}
			if (claxon2 == false && Math.Truncate(score.TimeRemaining) == 1)
			{
				audio.pitch = 0.6f;
				audio.clip = claxon;
				audio.Play();
				claxon2 = true;
			}
			if (claxon1 == false && Math.Truncate(score.TimeRemaining) == 0)
			{
				audio.pitch = 0.6f;
				audio.clip = claxon;
				audio.Play();
				claxon1 = true;
				
			}
		}
		else if (GameWorld.Paused)
		{
			GUI.skin = scoreSkin;
			GUI.Label(new Rect(130,70,80,40), "- Paused -");
		}
		else if (GameWorld.GameOver)
		{
			if (claxon0 == false)
			{
				audio.pitch = 1.0f;
				audio.volume = 1.0f;
				audio.clip = gameoverSound;
				audio.Play();
				claxon0 = true;
			}
			GUI.skin = scoreSkin;
			GUI.Label(new Rect(100,20,120,40), "- Game Over -");
			GUI.DrawTexture(new Rect(116,56,10,9), dollarSign);
			GUI.Label(new Rect(105,40,120,40), score.CurrentScore.ToString("D7"));
			if (cursorPos == 0)
			{
				GUI.Label(new Rect(100,100,120,40), "- Play Again -","LabelFocused");
			}
			else
			{
				GUI.Label(new Rect(100,100,120,40), "Play Again");
			}
			if (cursorPos == 1)
			{
				GUI.Label(new Rect(100,120,120,40), "- Main Menu -","LabelFocused");
			}
			else
			{
				GUI.Label(new Rect(100,120,120,40), "Main Menu");
			}
		}
	}
	void Accept()
	{
		Time.timeScale = 1;
		if (cursorPos == 0)
		{
			Application.LoadLevel("level");
		}
		else
		{
			Application.LoadLevel("Menu");
		}
	}
}
