using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDReset : MonoBehaviour {

    private Text m_resetText;
    private GameManager m_gameManager;

    void Awake()
    {
        m_resetText = GetComponent<Text>();
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {

        if(m_gameManager.currentLevelState != null)
        {
            m_resetText.text = string.Format("RESETS: {0}",m_gameManager.currentLevelState.Resets);
        }
	
	}
}
