using UnityEngine;
using System.Collections;

public class Menu : Singleton<Menu>
{

    public Texture titleBackground;
    public Texture boxBackground;

    public enum GameState
    {
        Title,
        PlayerSelect,
        Game,
        GameResults
    }

    bool[] playersReady = new bool[4];

    public Font font;

    public GameState CurrentState = GameState.Title;
    public int waitingPlayers = 0;
    public string title = string.Empty;
    GUIStyle style;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(BlinkText());
        StartCoroutine(WaitText());
        RandomTitleGenerator.GetTitle();
        style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.font = font;

        if (Menu.Instance != null)
            CurrentState = Menu.Instance.CurrentState;
    }

    bool blink = false;

    IEnumerator BlinkText()
    {
        while(true)
        {
            blink = true;
            yield return new WaitForSeconds(0.5f);
            blink = false;
            yield return new WaitForSeconds(0.5f);
        }
    }

    string waitText = "Waiting";
    IEnumerator WaitText()
    {
        while(true)
        {
            waitText = "Waiting";
            yield return new WaitForSeconds(0.4f);
            waitText = "Waiting.";
            yield return new WaitForSeconds(0.4f);
            waitText = "Waiting..";
            yield return new WaitForSeconds(0.4f);
            waitText = "Waiting...";
            yield return new WaitForSeconds(0.4f);
        }
    }

    bool ready = false;

    string startText = "3";
    IEnumerator StartGame()
    {
        ready = true;
        startText = "3";
        yield return new WaitForSeconds(0.75f);
        startText = "2";
        yield return new WaitForSeconds(0.75f);
        startText = "1";
        yield return new WaitForSeconds(0.75f);
        startText = "READY?";
        yield return new WaitForSeconds(0.75f);
        startText = RandomTitleGenerator.GetReady();
        yield return new WaitForSeconds(0.75f);
        GameManager.ActivePlayers = playersReady;
        CurrentState = GameState.Game;
        Application.LoadLevel("main2");
        yield return null;
    }


    void OnGUI()
    {
        switch (CurrentState)
        {
            case GameState.Title:
                style.normal.textColor = Color.red;
                style.fontSize = 36;
                style.alignment = TextAnchor.MiddleCenter;
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), titleBackground, ScaleMode.StretchToFill);
                Rect startBox = new Rect((Screen.width / 2)-64, (float)Screen.height - 65, 128, 32);
                title = RandomTitleGenerator.CurrentTitle;
                GUI.Label(new Rect((Screen.width / 2) - 100, 50, 200, 50), title,style);
                style.normal.textColor = Color.white;
                style.fontSize = 20;
                if(!blink) GUI.Button(startBox, "PRESS ENTER!", style);
                break;
            case GameState.PlayerSelect:
                //GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), titleBackground, ScaleMode.StretchToFill);
                Rect confirmBox = new Rect((((float)Screen.width) / 2f) - 64, (float)Screen.height - 70, 128, 32);
                DrawQuad(confirmBox, Color.black);
                style.normal.textColor = Color.white;
                if (!ready)
                {
                    if (teamsReady())
                    {
                        if (blink) GUI.Button(confirmBox, "PRESS ENTER!", style);
                    }
                    else
                    {
                        GUI.Button(confirmBox, waitText, style);
                    }
                }
                else
                {
                    GUI.Button(confirmBox, startText, style);
                }

               
                GUI.BeginGroup(new Rect(Screen.width * 0.4f, Screen.height * 0.5f, 300, 100));
                GUI.Label(confirmBox,"", style);
                style.normal.textColor = Color.red;
                GUILayout.Label(string.Format("P1 - {0}", (playersReady[0]) ? "READY" : "PRESS X "), style);
                GUILayout.Label(string.Format("P2 - {0}", (playersReady[1]) ? "READY" : "PRESS W "), style);
                style.normal.textColor = Color.blue;
                GUILayout.Label(string.Format("P3 - {0}", (playersReady[2]) ? "READY" : "PRESS O "), style);
                GUILayout.Label(string.Format("P4 - {0}", (playersReady[3]) ? "READY" : "PRESS UP"), style);
                GUI.EndGroup();
                break;
            case GameState.Game:
                break;
            case GameState.GameResults:
                GUI.BeginGroup(new Rect(Screen.width * 0.25f, Screen.height * 0.25f, 550, 100));
                style.fontSize = 36;
                style.normal.textColor = Color.red;
                GUILayout.Label("BATTLE RESULTS", style);
                style.fontSize = 20;
                style.normal.textColor = Color.white;
                GUILayout.Label("WINNER(S): " + GameManager.Winners, style);
                if (GameManager.PlayerSync > 0) GUILayout.Label( "SYNCRO: " + Mathf.Round((float)GameManager.PlayerSync).ToString() + "%", style);
                GUILayout.Label("RANK: " + GameManager.PlayerRank, style);
      
                GUI.EndGroup();
                 Rect box = new Rect((((float)Screen.width) / 2f) - 64, (float)Screen.height - 70, 128, 32);
                style.fontSize = 20;
                style.normal.textColor = Color.red;
                if(!blink) GUI.Button(box, "REVENGE?", style);
                break;
        }

    }

    void EndGame()
    {
        CurrentState = GameState.GameResults;
        Application.LoadLevel("menu");
    }

    void DrawQuad(Rect position, Color color)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        GUI.Box(position, GUIContent.none);
    }

    bool teamsReady()
    {
        bool team1 = false;
        bool team2 = false;
       for(int i=0; i < playersReady.Length-2; i++)
       {
           if (playersReady[i])
               team1 = true;
       }

       for (int j = 2; j < playersReady.Length; j++)
       {
           if (playersReady[j])
               team2 = true;
       }
       return (team1 && team2);
    }

    // Update is called once per frame
    void Update()
    {
        waitingPlayers = 0;
        for (int i = 0; i < playersReady.Length; i++)
        {
            if (playersReady[i])
                waitingPlayers += 1;
        }

        switch (CurrentState)
        {
            case GameState.Title:
                if (Input.GetKeyDown(KeyCode.Return))
                {
					title = string.Empty;
                    CurrentState = GameState.PlayerSelect;
                }

                if (Input.GetKeyDown(KeyCode.BackQuote))
                {
                    title = RandomTitleGenerator.GetTitle();
                }

                break;
            case GameState.PlayerSelect:


                if (Input.GetKeyDown(KeyCode.X))
                {
                    playersReady[0] = true;
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    playersReady[1] = true;
                }
                if (Input.GetKeyDown(KeyCode.O))
                {
                    playersReady[2] = true;
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    playersReady[3] = true;
                }

                if ((teamsReady()) && Input.GetKeyDown(KeyCode.Return))
                {
                    StartCoroutine(StartGame());
                }
                break;
            case GameState.Game:
                break;
            case GameState.GameResults:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    CurrentState = GameState.Title;
                    Application.LoadLevel("menu");
                }
                break;
        }
    }
}
