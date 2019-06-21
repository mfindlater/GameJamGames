using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ATB : MonoBehaviour {

    public Battle battle;
    public Slider playerBar;
    public Slider enemyBar;

	void Update () {
        playerBar.value = battle.player.Act;
        enemyBar.value = battle.enemy.Act;
	}
}
