using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour {

    private Player m_player;
    private Text m_text;

	void Awake () {
        m_player = GameObject.Find("Player").GetComponent<Player>();
        m_text = GetComponent<Text>();
	}
	
	void Update () {
        Debug.Log(m_player.Mana);
        m_text.text = string.Format("MANA: {0}", m_player.Mana.ToString("000"));
	
	}
}
