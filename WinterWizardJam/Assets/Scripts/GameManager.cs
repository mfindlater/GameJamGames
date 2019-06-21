using System;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public const string GameSaveFileName = "";
    public bool GameSaveFound = false;
    public GameState gameState;
    public string currentLevelSceneLoaded = string.Empty;
    public LevelState currentLevelState;
    public float timeToLoadSeconds = 2;
    public LevelDatabase levelDatabase;

    public bool levelCompleted;

	void Awake ()
    {
        DontDestroyOnLoad(transform.gameObject);
	}

    void Start()
    {
        //Check if save file exists.
        GameSaveFound = LoadGame();
    }

    public void PlayerFailed()
    {
        currentLevelState.Resets += 1;

    }

    void Update()
    {
        if (currentLevelState != null && levelCompleted == false)
        {
            currentLevelState.Time += Time.deltaTime;
        }
    }

    public bool LoadGame()
    {
        string filepath = Path.Combine(Application.persistentDataPath, GameSaveFileName);

        if (File.Exists(filepath))
        {
            using (var stream = File.Open(filepath, FileMode.Open))
            {
                var bf = new BinaryFormatter();
                gameState = (GameState)bf.Deserialize(stream);
            }

            return true;
        }
        return false;
    }

    public void SaveGame()
    {
        string filepath = Path.Combine(Application.persistentDataPath, GameSaveFileName);

        using (var stream = File.OpenWrite(filepath))
        {
            var bf = new BinaryFormatter();
            bf.Serialize(stream, gameState);
        }

    }

    public string GetNextLevelScene()
    {
        for(int i=0; i < levelDatabase.levels.Length; i++)
        {
            if(levelDatabase.levels[i].sceneName.Equals(currentLevelSceneLoaded))
            {
                if(levelDatabase.levels.Length > i+1)
                {
                    return levelDatabase.levels[i + 1].sceneName;
                }
            }
        }
        return string.Empty;
    }

    public IEnumerator StartLevel(string levelSceneName)
    {
        if(!string.IsNullOrEmpty(currentLevelSceneLoaded))
        {
            SceneManager.UnloadScene(currentLevelSceneLoaded);
        }

        yield return new WaitForSeconds(timeToLoadSeconds);

        SceneManager.LoadScene(levelSceneName, LoadSceneMode.Additive);
        currentLevelSceneLoaded = levelSceneName;
   
    
        currentLevelState = new LevelState();
        levelCompleted = false;

        Player.HandlingInput = true;
        
        while(!levelCompleted)
        {
            yield return null;
        }

        currentLevelState.Completed = true;
    }
}
