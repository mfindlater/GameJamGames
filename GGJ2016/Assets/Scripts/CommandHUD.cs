using UnityEngine;
using System.Collections;

public class CommandHUD : MonoBehaviour {

    public Battle battle;
    public BattleText battleText;

    public void SelectCommand(int index)
    {
        var skill = battle.player.Skills[index];
        string say = skill.Use(battle.player, battle.enemy);
        battleText.SayText(say, 4);
    }
}
