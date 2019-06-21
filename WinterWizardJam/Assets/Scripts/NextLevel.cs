using UnityEngine;
using System.Collections;

public class NextLevel : MonoBehaviour {

    private GameManager m_gameManager;

    // Use this for initialization
    void Awake () {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	public void StartNextLevel()
    {
        string levelScene = m_gameManager.GetNextLevelScene();

        if (!string.IsNullOrEmpty(levelScene))
        {
            m_gameManager.SendMessage("StartLevel", levelScene);
        }
    }
}
