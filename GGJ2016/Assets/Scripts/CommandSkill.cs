using UnityEngine;
using UnityEngine.UI;

public class CommandSkill : MonoBehaviour {

    public Battle battle;
    private Button button;
    private Text text;
    public int skillIndex;

	void Awake ()
    {
        button = GetComponent<Button>();
        text = button.GetComponentInChildren<Text>();
	}

	
	void Update()
    {
        if (battle.player.Skills.Count > skillIndex)
        {
            text.text = string.Format("{0}:{1} ", battle.player.Skills[skillIndex].SkillName.ToString(), battle.player.Skills[skillIndex].Cost);
        }
	}
}
