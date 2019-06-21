using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class Menu : MonoBehaviour {

	private int cursorPos = 0;
	private int menuState = 0;
	public string mainSceneName = "";
	public int cursorMax = 3;
	public GUISkin gSkin;
	public float baseWidth = 320;
	public float baseHeight = 180;
	private Vector2 UIScale;
	private string newPName = "";
	private ProfileManager pManager;
	public AudioClip selectSound;
	public AudioClip confirmSound;
	public string[] highScoreNames;
	public int[] highScores;
	private string[] topNames;
	private int[] topScores;
	private List<Profile> highScoreList = new List<Profile>();
	private bool listSetUp;
    InputDevice input;
	// Use this for initialization
	void Start () {
		listSetUp = false;
		cursorPos = 0;
		pManager = GameObject.Find("Profile_Manager").GetComponent<ProfileManager>();
        InputManager.Setup();
        input = InputManager.ActiveDevice;
	}
	
	// Update is called once per frame
	void Update () {
		MenuInput();
        InputManager.Update();
	}

	void OnGUI ()
	{
		GUI.skin = gSkin;
		UIScale = new Vector2(Screen.width / baseWidth, Screen.height / baseHeight);
		GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (UIScale.x, UIScale.y, 1));
		if (menuState == 0)
		{
			GUI.Label(new Rect(40,20,240,20),"GRAVITY BLOKE");
			if (cursorPos == 0)
			{
				GUI.Label(new Rect(60,80,200,20),"- Play -","LabelFocused");
			}
			else
			{
				GUI.Label(new Rect(60,80,200,20),"Play");
			}
			if (cursorPos == 1)
			{
				GUI.Label(new Rect(60,100,200,20),"- High Scores -","LabelFocused");
			}
			else
			{
				GUI.Label(new Rect(60,100,200,20),"High Scores");
			}
			if (cursorPos == 2)
			{
				GUI.Label(new Rect(60,120,200,20),"- Credits -","LabelFocused");
			}
			else
			{
				GUI.Label(new Rect(60,120,200,20),"Credits");
			}
			if (cursorPos == 3)
			{
				GUI.Label(new Rect(60,140,200,20),"- Quit -","LabelFocused");
			}
			else
			{
				GUI.Label(new Rect(60,140,200,20),"Quit");
			}
		}
		else if (menuState == 1)
		{
			GUI.Label(new Rect(40,20,240,20),"HIGH SCORES");
			GUI.Label(new Rect(20,50,100,20),highScoreList[0].name,"LabelLeft");
			GUI.Label(new Rect(100,50,180,20),highScoreList[0].highScore.ToString("D7"),"LabelRight");
			GUI.Label(new Rect(20,70,100,20),highScoreList[1].name,"LabelLeft");
			GUI.Label(new Rect(100,70,180,20),highScoreList[1].highScore.ToString("D7"),"LabelRight");
			GUI.Label(new Rect(20,90,100,20),highScoreList[2].name,"LabelLeft");
			GUI.Label(new Rect(100,90,180,20),highScoreList[2].highScore.ToString("D7"),"LabelRight");
			GUI.Label(new Rect(20,110,100,20),highScoreList[3].name,"LabelLeft");
			GUI.Label(new Rect(100,110,180,20),highScoreList[3].highScore.ToString("D7"),"LabelRight");
			GUI.Label(new Rect(20,130,100,20),highScoreList[4].name,"LabelLeft");
			GUI.Label(new Rect(100,130,180,20),highScoreList[4].highScore.ToString("D7"),"LabelRight");
		}
		else if (menuState == 2)
		{
			GUI.Label(new Rect(40,20,240,20),"CREDITS");
			GUI.Label(new Rect(20,50,100,40),"Matthew \n \n Findlater","LabelSmall");
			GUI.Label(new Rect(120,50,180,40),"Programmer - Designer","LabelSmall");
			GUI.Label(new Rect(20,80,100,40),"Du-Marc Mills","LabelSmall");
			GUI.Label(new Rect(120,80,180,40),"Music & Sound","LabelSmall");
			GUI.Label(new Rect(20,120,100,40),"Steve Tamayo","LabelSmall");
			GUI.Label(new Rect(120,120,180,40),"Programmer - Designer","LabelSmall");
			GUI.Label(new Rect(20,160,100,40),"John Benge","LabelSmall");
			GUI.Label(new Rect(120,160,180,40),"Programmer - Designer","LabelSmall");
            GUI.Label(new Rect(20, 140, 100, 40), "Ivy Wilson", "LabelSmall");
            GUI.Label(new Rect(120, 140, 180, 40), "Artist", "LabelSmall");
		}
		else if (menuState == 3)
		{
			GUI.Label(new Rect(60,80,200,20),"Create A Profile");
			GUI.SetNextControlName("NameField");
			newPName = GUI.TextField(new Rect(60,110,200,20),newPName, 12);
			GUI.FocusControl("NameField");
			Event e = Event.current;
        	if ((Event.current.type == EventType.KeyUp) && e.keyCode == KeyCode.Return)
			{
        		NameEnter();
    		}
		}
	}

	void NameEnter() {
		pManager.NewProfile(newPName);
        menuState = 0;
	}
	void MenuInput () {

		if (menuState != 3)
		{
			if ((Input.GetKeyDown(KeyCode.DownArrow) || input.LeftStick.Down.WasPressed || input.DPadDown.WasPressed) && cursorPos < cursorMax)
			{
				cursorPos += 1;
				audio.clip = selectSound;
				audio.Play();
			}
			if ((Input.GetKeyDown(KeyCode.UpArrow) || input.LeftStick.Up.WasPressed || input.DPadUp.WasPressed) && cursorPos > 0)
			{
				cursorPos -= 1;
				audio.clip = selectSound;
				audio.Play();
			}
			if (Input.GetKeyDown(KeyCode.Return) || input.Action1.WasPressed)
			{
				if (menuState == 0)
				{
					Accept();
					audio.clip = confirmSound;
					audio.Play();
				}
			}
			if (Input.GetKeyDown(KeyCode.Escape) || input.Action2.WasPressed)
			{
				Back();
				audio.clip = selectSound;
				audio.Play();
			}
		}
	}

	void Accept() {
		switch (cursorPos)
		{
			case 0:
				Application.LoadLevel(mainSceneName);
			break;
			case 1:
				if (!listSetUp)
				{
					ComputeHighScores();
				}
				menuState = 1;
			break;
			case 2:
				menuState = 2;
			break;
			case 3:
				Application.Quit();
			break;
			default:
			break;
		}
	}
	void Back ()
	{
		menuState = 0;
	}

	public void OnNewProfile()
	{
		menuState = 3;

	}

	void ComputeHighScores()
	{
		CreateList();
		SortList();
		listSetUp = true;
	}
	void CreateList()
	{
		for (int i = 0;i<highScores.Length;i++)
		{
			Profile p = new Profile();
			p.name = highScoreNames[i];
			p.highScore = highScores[i];
			highScoreList.Add(p);
		}
		highScoreList.Add(pManager.playerProf);
	}
	void SortList()
	{
		HighScoreComparer hc = new HighScoreComparer();
		highScoreList.Sort(hc);
		highScoreList.Reverse();
	}

}

public class HighScoreComparer: IComparer<Profile>
{
	public int Compare(Profile x, Profile y)
    {
    	if (x == null)
        {
            if (y == null)
            {
                // If x is null and y is null, they're 
                // equal.  
                return 0;
            }
            else
            {
                // If x is null and y is not null, y 
                // is greater.  
                return -1;
            }
        }
        else
        {
            // If x is not null... 
            // 
            if (y == null)
                // ...and y is null, x is greater.
            {
                return 1;
            }
            else
            {
            	if (x.highScore > y.highScore)
            	{
            		return 1;
            	}
            	else if (y.highScore > x.highScore)
            	{
            		return -1;
            	}
            	else
            	{
            		return x.name.CompareTo(y.name);
            	}
            }
        }
    }

}

