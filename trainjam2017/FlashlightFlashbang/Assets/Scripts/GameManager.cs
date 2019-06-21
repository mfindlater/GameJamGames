using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.Cameras;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameStateManager
{
    const int MaxPlayers = 4;

    public PlayerEvent PlayerWon = new PlayerEvent();

    public UnityEvent GameOver;
    

    private void OnPlayerReady(PlayerBehavior player)
    {
        // spawn a player

        if (freeSpots.Count > 0)
        {
            var transform = GetFreeSpawnPoint();

            player.transform.position = transform.position;
            player.transform.rotation = transform.rotation;
        }
    }

    private List<PlayerBehavior> players;

    private Dictionary<string, GameState> states = new Dictionary<string, GameState>();

    private GameState CurrentState;

    Stack<Transform> freeSpots = new Stack<Transform>();

    PlayerBehavior winner;

    public void AddPlayer(PlayerBehavior player)
    {
        players.Add(player);
        player.PlayerReady.AddListener(OnPlayerReady); 
    }

    public void Initialize()
    {
        players = new List<PlayerBehavior>(MaxPlayers);
        SetFreeSpawnPoints();
    }

    public void Shuffle<T>(T[] deck)
    {
        for (int i = 0; i < deck.Length; i++)
        {
            T temp = deck[i];
            int randomIndex = UnityEngine.Random.Range(0, deck.Length);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    public void SetFreeSpawnPoints()
    {
        var spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint").Select(g => g.transform).ToArray();

        Shuffle(spawnPoints);

        freeSpots = new Stack<Transform>(spawnPoints);
    }

    public Transform GetFreeSpawnPoint()
    {
        return freeSpots.Pop();
    }

    public void SetWinner(PlayerBehavior p)
    {
        p.Won();
    }

    public void Update()
    {
        CurrentState.Update();
    }

    public PlayerBehavior[] GetRemainingPlayers()
    {
        return players.Where(p => p.playerState.IsReady && !p.playerState.IsDead).ToArray();
    }

    public int PlayersRemainingCount()
    {
        int playersStillAlive = 0;

        for(int i=0; i < players.Count; i++)
        {
            if(!players[i].playerState.IsDead && players[i].playerState.IsReady)
            {
                playersStillAlive += 1;
            }
        }
        return playersStillAlive;
    }

    public int PlayersReadyCount()
    {
        int readyPlayers = 0;
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].playerState.IsReady)
                readyPlayers += 1;
        }

        return readyPlayers;
    }

    public void AddState(string stateName, GameState state)
    {
        states.Add(stateName, state);
        state.Game = this;
    }

    public void ChangeState(string stateName)
    {
        if (CurrentState != null)
        {
            var last = CurrentState;
            last.End();
        }

        CurrentState = states[stateName];

        CurrentState.Start();
    }

}

public class WaitingForPlayers : GameState
{
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        if (Game.PlayersReadyCount() >= 2)
        {
                Game.ChangeState("Playing");
        } 
    }

    public override void End()
    {
        base.End();
    }
}

public class Playing : GameState
{

    public override void Start()
    {
        Debug.Log("Play State");
    }

    public override void Update()
    {
        if(Game.PlayersRemainingCount() == 1)
        {
            Game.SetWinner(Game.GetRemainingPlayers().First());
            
            Game.ChangeState("GameOver");
        }
    }
}

public class GameOver : GameState
{
    private float timer = 25;

    public override void Start()
    {
      
        ///
    }

    public override void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
            Application.Quit();
    }
}

public abstract class GameState
{
    public GameStateManager Game { get; set; }
    public string Name { get; set; }

    public virtual void Start()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void End()
    {

    }
}

public class GameManager : MonoBehaviour {

    public GameStateManager Game { get; set; }
    public GameState CurrentState { get; private set; }
    public static GameManager instance;

    public void SetupPlayer(PlayerBehavior player)
    {
        //get playerstate from playerbehavior
        //add to gamestatemanager
        //Attach camera
        Game.AddPlayer(player);
        var autoCamera = GameObject.Find(string.Format("Player{0}_Camera", player.playerNumber + 1)).GetComponentInChildren<AutoCam>();
        autoCamera.SetTarget(player.transform);
    }

    void Awake() {
        DontDestroyOnLoad(this);
        instance = this;

        Game = new GameStateManager();
        Game.Initialize();

        Game.AddState("WaitingForPlayers", new WaitingForPlayers());
        Game.AddState("Playing", new Playing());
        Game.AddState("GameOver", new GameOver());

        Game.ChangeState("WaitingForPlayers");
	}
	
	void Update () {
        Game.Update();
	}
}
