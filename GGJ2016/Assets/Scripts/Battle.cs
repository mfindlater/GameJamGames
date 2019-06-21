using UnityEngine;
using System.Collections;

public class Battle : MonoBehaviour {

    public Combatant player;
    public GameObject playerSprite;
    public GameObject enemySprite;
    public Combatant enemy;
    public BattleText battleText;
    private GameManager gameManager;
    bool over;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

	void OnEnable () {
        player.Initialize();
        enemy.Initialize();
        playerSprite.SetActive(true);
        enemySprite.SetActive(true);
        over = false;
        StartCoroutine(EnemyAI());

    }
	
    void Update()
    {
        if (over)
            return;

        if(enemy.Health <= 0)
        {
            over = true;
            StartCoroutine(EnemyDefeated());
        }

        if(player.Health <= 0)
        {
            over = true;
            StartCoroutine(PlayerDefeated());
        }

        Tick(player);
        Tick(enemy);
    }

    IEnumerator EnemyDefeated()
    {
        enemySprite.SetActive(false);
        float dwell = 3;
        battleText.SayText(enemy.Name + " was defeated", dwell);
        yield return new WaitForSeconds(dwell);
        gameManager.EndBattle(true);
    }

    IEnumerator PlayerDefeated()
    {
        playerSprite.SetActive(false);
        float dwell = 3;
        battleText.SayText(player.Name + " was defeated", dwell);
        yield return new WaitForSeconds(dwell);
        gameManager.EndBattle(false);
    }

    private void Tick(Combatant combatant)
    {
        combatant.Act += (100 - (100 - combatant.Stats.Speed)) * Time.deltaTime ;
        combatant.Act = Mathf.Clamp(combatant.Act, 0, Combatant.Ready);
    }

    IEnumerator EnemyAI()
    {
        while (enemy.Health > 0)
        {
            yield return new WaitForSeconds(Random.Range(4,8));

            int skills = Random.Range(1, 4);

            switch(skills)
            {
                case 1:
                    if (enemy.Act < 10)
                        break;
                    player.Health -= 10;
                    enemy.Act -= 10;
                    battleText.SayText(string.Format("Game used Crash, did 10 damage."),3);
                    break;
                case 2:
                    if (enemy.Act < 50)
                        break;
                    enemy.Health = Mathf.Clamp(enemy.Health + 50, 0, 100);
                    enemy.Act -= 50;
                    battleText.SayText(string.Format("Game used DLC, game restored health."), 3);
                    break;
                case 3:
                    if (enemy.Act < 100)
                        break;
                    player.Health -= 80;
                    enemy.Act -= 100;
                    battleText.SayText(string.Format("Game used Water Puzzle, did 80 damage."),3);
                    break;
            }
        }
    }

	void OnDisable () {
	

	}
}
