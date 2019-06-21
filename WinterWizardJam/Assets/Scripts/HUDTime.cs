using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDTime : MonoBehaviour {

    private Text m_timeText;
    private GameManager m_gameManager;

    void Awake()
    {
        m_timeText = GetComponent<Text>();
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update () {
        if (m_gameManager.currentLevelState != null)
        {
            m_timeText.text = string.Format("TIME: {0}", m_gameManager.currentLevelState.Time.ToString("000"));
        }
    }
}
