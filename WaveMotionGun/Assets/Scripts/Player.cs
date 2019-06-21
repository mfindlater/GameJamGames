using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    public bool charging = false;
    public float chargePower = 0f;
    public float chargeRate = 2f;
    public float maximumCharge = 100f;
    public float speed = 10f;
    public float laserLength = 50f;
    public float laserDissapearTime = 0.5f;
    public float invincibleTime = 1f;
    public float respawnTime = 2.5f;

    public int lives = 3;

    public Rect bounds;
    public Arena arena;
    public Laser laser;
    public AudioClip fireLaserSound;
    public AudioClip chargeLaserSound;
    public AudioClip fullyChargedSound;

    public Explosion explosion;


    private bool laserReady = true;
    private bool laserOnScreen = false;
    public bool alive = true;
    private bool invincible = false;

    private Vector2 direction = new Vector2(1, 0);

    private Transform m_transform;
    private LineRenderer m_lineRenderer;
    private AudioSource m_audioSource;
    private Laser activeLaser;
    private Transform spawnPoint;

    public Portrait popup;

    public bool IsFullyCharged
    {
        get { return chargePower >= maximumCharge; }
    }

    public int enemiesOnScreen = 0;

    public EnemySpawnManager enemySpawnManager;

    private List<Laser> myLasers = new List<Laser>();

    // Use this for initialization
    void Start()
    {
        m_transform = GetComponent<Transform>();
        m_lineRenderer = GetComponent<LineRenderer>();
        m_audioSource = GetComponent<AudioSource>();

        explosion.CreatePool(4);
        laser.CreatePool(4);

        spawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
    }

    IEnumerator Invincibility()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibleTime);
        invincible = false;
    }

    IEnumerator FireLaser()
    {
        if(fireLaserSound != null)
        {
            m_audioSource.clip = fireLaserSound;
            m_audioSource.Play();
        }

        laserOnScreen = true;

        activeLaser = laser.Spawn();

        activeLaser.transform.position = m_transform.position;

        myLasers.Add(activeLaser);
        
        var ray = new Ray2D(m_transform.position, direction);
        
        var start = m_transform.position + new Vector3(4.5f,0);
        var end = ray.GetPoint(laserLength);

        var positions = new Vector3[] { start, end };

        m_lineRenderer.SetPositions(positions);
       

        yield return new WaitForSeconds(laserDissapearTime);
        positions = new Vector3[] {Vector3.zero, Vector3.zero };
        m_lineRenderer.SetPositions(positions);
        m_lineRenderer.numCapVertices = 0;
        laserOnScreen = false;
    }

    Vector3 UpdatePosition()
    {
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float y = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        var v = new Vector3(x, y);

        v = Vector3.ClampMagnitude(v, speed * Time.deltaTime);

        return v;

    }

    Portrait currentPopUp;

    private void PopUp()
    {
          if(currentPopUp != null)
          {
                currentPopUp.Recycle();
          }

        currentPopUp = popup.Spawn();

        currentPopUp.transform.position = new Vector3(-7.65f, -3.65f);
    }

    private void CheckArenaBounds()
    {
        if (m_transform.position.x + (bounds.width / 2) > arena.transform.position.x + (arena.bounds.width / 2))
        {
            float x = arena.transform.position.x + (arena.bounds.width / 2) - (bounds.width / 2);
            m_transform.position = new Vector3(x, m_transform.position.y);
        }
        else if (m_transform.position.x - (bounds.width / 2) < arena.transform.position.x - (arena.bounds.width / 2))
        {
            float x = arena.transform.position.x - (arena.bounds.width / 2) + (bounds.width / 2);
            m_transform.position = new Vector3(x, m_transform.position.y);
        }

        if (m_transform.position.y + (bounds.height / 2) > arena.transform.position.y + (arena.bounds.height / 2))
        {
            float y = arena.transform.position.y + (arena.bounds.height / 2) - (bounds.height / 2);
            m_transform.position = new Vector3(m_transform.position.x, y);
        }
        else if (m_transform.position.y - (bounds.height / 2) < arena.transform.position.y - (arena.bounds.height / 2))
        {
            float y = arena.transform.position.y - (arena.bounds.height / 2) + (bounds.height / 2);
            m_transform.position = new Vector3(m_transform.position.x, y);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, bounds.size);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!alive)
            return;

        CheckArenaBounds();
        var p = UpdatePosition();
        m_transform.Translate(p);


        if(laserOnScreen)
        {
            Ray2D ray = new Ray2D(m_transform.position + new Vector3(1.5f, 0), direction);

         
            m_lineRenderer.SetPosition(0, ray.origin);
            m_lineRenderer.SetPosition(1, ray.GetPoint(laserLength));
            m_lineRenderer.numCapVertices = 90;



            if (activeLaser != null)
                activeLaser.transform.position = m_transform.position;
        }

        if (charging)
        {
            chargePower += chargeRate * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && laserReady)
        {
            charging = true;
            StopCoroutine("ChargingSounds");
            StartCoroutine("ChargingSounds");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (IsFullyCharged)
            {
                StopCoroutine("FireLaser");
                StartCoroutine("FireLaser");
            }
            else
            {
                PopUp();
            }

            chargePower = 0;
            charging = false;
        }

        


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag.Equals("Enemy") || other.tag.Equals("EnemyBullet"))
        {
            if (!invincible)
            {
                StartCoroutine(Kill());
            }
            //if(other != null)
                //other.SendMessage("Kill", false);
        }
    }

    private void CheckGameOver()
    {
        if(lives <= 0)
        {
            StopAllCoroutines();
            //SceneManager.LoadScene("GameOver");
        }
    }

    public IEnumerator ChargingSounds()
    {
        while(charging && !IsFullyCharged && alive)
        {
            yield return new WaitForSeconds(0.25f);

            if(chargeLaserSound != null)
            {
                m_audioSource.clip = chargeLaserSound;
                m_audioSource.Play();
            }
        }

        if (IsFullyCharged)
        {
            if(fullyChargedSound !=null)
            {
                m_audioSource.clip = fullyChargedSound;
                m_audioSource.Play();
            }
        }
    }

    public IEnumerator Kill()
    {
        alive = false;

        for(int i=0; i < myLasers.Count; i++)
        {
            myLasers[i].RecycleAll();
        }


        if(explosion != null)
        {
            explosion.Spawn(m_transform.position);
        }

        m_transform.position = new Vector3(100, 100);
        chargePower = 0;
        lives--;

        enemySpawnManager.KillAll(); 

        yield return new WaitForSeconds(respawnTime);

        CheckGameOver();

        if (lives <= 0)
            yield return null;

        m_transform.position = spawnPoint.transform.position;
        alive = true;
        StartCoroutine(Invincibility());

        
    }
}
