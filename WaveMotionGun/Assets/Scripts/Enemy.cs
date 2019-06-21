using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Kamikaze,
    StopAndShoot,
    Straightforward,
    Wildcard
}

public class Enemy : MonoBehaviour
{
    private string currentSprite = "enemy_ship_1";

    public IEnemyKillListener killListener;

    protected Transform m_transform;
    public Vector3 direction = new Vector3(-1, 0);
    public float speed = 5f;

    public float edge = 5;

    public EnemyType enemyType;

    Player player;

    public Bullet bulletPrefab;

    public bool alive = true;

    public float timeBetweenShots = 2f;

    public Explosion explosionPrefab;

    private bool shouldUpdate = true;

    private bool invincible = false;

    List<Bullet> bullets = new List<Bullet>();

    private float xMin = -20;

    private AudioSource m_audioSource;
    public AudioClip fireSound;

    private Dictionary<EnemyType, string> spriteMap = new Dictionary<EnemyType, string>();

    public int xMax = 8;

    int mod = 1;

    void OnEnable()
    {
        alive = true;
        invincible = true;
        shouldUpdate = true;

        if(Random.Range(0,100) + 1 > 50)
        {
            mod = -mod;
        } 
    }

    // Use this for initialization
    void Awake()
    {
        bulletPrefab.CreatePool(20);

        spriteMap.Add(EnemyType.Kamikaze, "enemy_ship_1");
        spriteMap.Add(EnemyType.StopAndShoot, "enemy_ship_2");
        spriteMap.Add(EnemyType.Straightforward, "enemy_ship_3");
        spriteMap.Add(EnemyType.Wildcard, "enemy_ship_4");

        m_transform = GetComponent<Transform>();
        m_audioSource = GetComponent<AudioSource>();
      
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        explosionPrefab.CreatePool(10);

        SetType(EnemyType.Kamikaze);
    }

    public void SetType(EnemyType type)
    {
        string spriteName = spriteMap[type];
        var old = m_transform.Find(currentSprite).gameObject;
        old.SetActive(false);

        var act = m_transform.Find(spriteName).gameObject;
        act.SetActive(true);

        enemyType = type;
    }

    // Update is called once per frame
    void Update()
    {
        if (!shouldUpdate)
            return;

        switch (enemyType)
        {
            case EnemyType.Kamikaze:
                SeekPlayer();
                break;
            case EnemyType.StopAndShoot:
                if (m_transform.position.x > edge)
                    GoStraight();
                else
                    StartCoroutine("Shoot");
                break;
            case EnemyType.Straightforward:
                GoStraight();
                break;
            case EnemyType.Wildcard:
                SineWave();
                break;
        }

        if(m_transform.position.x < xMin)
        {
            Kill(false);
        }

        if (m_transform.position.x < xMax && invincible)
            invincible = false;
    }

    const float it = 1.1f;

    private void SineWave()
    {
        var newPosition = m_transform.position;
        newPosition.y += mod * Mathf.Sin(Time.time * it) * Time.deltaTime;
        newPosition.x += direction.x * speed * Time.deltaTime;

        m_transform.position = newPosition;
    }

    private void GoStraight()
    {
        m_transform.position += direction * speed * Time.deltaTime;
    }

    private void SeekPlayer()
    {
        if(player.alive)
            m_transform.position += (player.transform.position - m_transform.position).normalized * speed * Time.deltaTime;
    }

    private IEnumerator Shoot()
    { 
        shouldUpdate = false; 
        while (alive && player.alive)
        {
            if(fireSound != null)
            {
                m_audioSource.clip = fireSound;
                m_audioSource.Play();
            }

            var bullet = bulletPrefab.Spawn();
            bullet.Set(m_transform.position, (player.transform.position - m_transform.position).normalized, 2);
            bullets.Add(bullet);

            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    public void Kill(bool givePoints)
    {
        if (invincible)
            return;

        StopAllCoroutines();
        killListener.OnKill(this);
        alive = false;
        explosionPrefab.Spawn(m_transform.position);
        gameObject.Recycle();

        for(int i=0; i < bullets.Count; i++)
        {
            bullets[i].Kill(false);
        }
    }
}
