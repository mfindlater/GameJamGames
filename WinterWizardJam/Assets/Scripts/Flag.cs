using UnityEngine;
using UnityEngine.UI;

public class Flag : MonoBehaviour
{
    private GameManager m_gameManager;
    public GameObject resultPanel;

    void Awake()
    {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            m_gameManager.levelCompleted = true;
            collider.gameObject.SendMessage("ResetPhysics");

            var resetText = resultPanel.transform.FindChild("ResetText").GetComponent<Text>();
            resetText.text = string.Format("RESETS: {0}", m_gameManager.currentLevelState.Resets);

            var timeText = resultPanel.transform.FindChild("TimeText").GetComponent<Text>();
            timeText.text = string.Format("TIME: {0}", m_gameManager.currentLevelState.Time.ToString("000"));

            resultPanel.SetActive(true);
            Player.HandlingInput = false;

            if (IceTrail.activeIceTrail != null)
            {
                IceTrail.activeIceTrail.Recycle();
            }


        }

    }
}
