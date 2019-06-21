using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HP : MonoBehaviour {

    public Battle battle;
    public Text playerHealth;
    public Text enemyHealth;

	void Awake () {
       
	}
	
	void Update () {
        playerHealth.text = string.Format("HP:{0}/{1}", battle.player.Health, battle.player.Stats.MaximumHealth);
        enemyHealth.text = string.Format("HP:{0}/{1}", battle.enemy.Health, battle.enemy.Stats.MaximumHealth);
    }
}
