using UnityEngine;
using System.Collections;
using System;

public class Enemy : MonoBehaviour {


    private Gravity gravity;
    
    private Ray2D look;
    public float LookDistance;
    public float ShootDistance = 200;
    public bool seePlayer = false;
    public float MemoryTimer = 1f;
    public float Speed = 200;
    public float MaxDistanceToPlayer = 48;

    public Vector2 Direction = new Vector2(1,0);
    private Transform curWaypoint;

    private Player player;
    private bool grounded;
    private float groundedRadius = 7f;
    public LayerMask GroundLayer;
    private Transform groundCheck;

    private Lassoed lassoed;
    private Health health;
    private bool facingRight = true;
    public Action Incapacitated;
    public float ShootCooldownTimer = 0.5f;
    private bool canFire = true;

    public GameObject Bullet;

    public float ShootForce = 400;

    private Score score;

	// Use this for initialization

    public int Bounty = 100;
    public int AliveMult = 3;

    private bool isDead = false;

	private Rigidbody2D jointedPlayer;
    public float ShootDelay = 0.6f;
    public bool bountyTarget;
    public float bTimer1 = 30;
    public float bTimer2 = 45;
    public float bTimer3 = 60;
    private float spawnTime;
    private bool reduced1 = false;
    private bool reduced2 = false;
	private bool reduced3 = false;
	
	public Animator animator;

	static int DeadState = Animator.StringToHash("Base Layer.Dead");
	
    public bool IsDead
    {
        get { return isDead; }
    }

    public bool IsGrounded
    {
        get { return grounded; }
    }

    public bool IsLassoed
    {
        get { return Lassoed.isLassoed; }
    }

	public Lassoed Lassoed
	{
		get { return lassoed; }
	}

    public bool CanSeePlayer
    {
        get { return seePlayer; }
    }

    public Gravity Gravity
    {
        get { return gravity; }
    }

    void Awake()
    {
		
		animator = GetComponent<Animator>();
		jointedPlayer = GameObject.FindGameObjectWithTag("Player").rigidbody2D;
		GetComponent<SpringJoint2D>().connectedBody = jointedPlayer;
		GetComponent<DistanceJoint2D>().connectedBody = jointedPlayer;
        gravity = GetComponent<Gravity>();
        groundCheck = transform.Find("GroundCheck");
        lassoed = GetComponent<Lassoed>();
        health = GetComponent<Health>();
        health.HealthReachedZero += Die;
        score = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Score>();
        StartCoroutine(Think());
    }
    void Start()
    {
        spawnTime = Time.time;
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, GroundLayer);
        gravity.Apply(rigidbody2D);

        Vector2 dir = Vector2.zero;

        if (facingRight)
            dir = new Vector2(1, 0);
        else
            dir = new Vector2(-1, 0);

       RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, LookDistance, 1 << LayerMask.NameToLayer("Player"));
		RaycastHit2D hit2 = Physics2D.Raycast(transform.position, dir, LookDistance, 1 << LayerMask.NameToLayer("WayPoint"));


		if(hit.collider != null && hit.collider.tag.Equals("Player"))
        {
            StopCoroutine("SeePlayer");
            StartCoroutine("SeePlayer");
            player = hit.collider.gameObject.GetComponent<Player>();
        }

		AnimatorStateInfo currentBaseState = animator.GetCurrentAnimatorStateInfo(0);
		
		if (currentBaseState.nameHash == DeadState) {
			DestroySelf();
		}
	}
	
	IEnumerator Think()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / 55);

            while (!IsDead && IsGrounded && !IsLassoed)
            {
                if (!CanSeePlayer)
                {
                    Patrol();
                }
                else
                {
                    ChasePlayer();
                    yield return new WaitForSeconds(ShootDelay);
                    Shoot();
                }

                yield return null;
            }
        }
    }

	
	// Update is called once per frame
	void Update () {

        var dir = rigidbody2D.velocity.normalized;

		if(rigidbody2D.velocity.x == 0)
		{
			animator.SetBool("Run", false);
		} else
		{
			animator.SetBool("Run", true);
		}

        if(dir.x > 0 && !facingRight)
        {
            FlipX();
        } 
        else if(dir.x < 0 && facingRight)
        {
            FlipX();
        }

        if (!grounded)
        {
            //Debug.Log("In Air");
        }
        //if bountyTarget, reduce bounty
        if (bountyTarget)
        {
            if (!reduced1 && Time.time >= spawnTime + bTimer1)
            {
                transform.Find("dollarSign3").gameObject.SetActive(false);
                Bounty = Bounty/2;
                reduced1 = true;
            }
            if (!reduced2 && Time.time >= spawnTime + bTimer2)
            {
                transform.Find("dollarSign2").gameObject.SetActive(false);
                Bounty = Bounty/2;
                reduced2 = true;
            }
            if (!reduced3 && Time.time >= spawnTime + bTimer3)
            {
                print("assdfsadfsdfsdf");
                transform.Find("dollarSign1").gameObject.SetActive(false);
                Bounty = Bounty/2;
                reduced3 = true;
            }
        }
	}

    void OnDrawGizmos()
    {
        var dir = rigidbody2D.velocity.normalized * LookDistance;
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, dir);
    }

    private void Patrol()
    {
        rigidbody2D.velocity = Direction * Speed;
    }

    private Vector2 CalculateAim()
    {
        Vector2 aim = Vector2.zero;

        if (facingRight)
            aim = new Vector2(1, 0);
        else
            aim = new Vector2(-1, 0);
        return aim;
    }

    private void Shoot()
    {
        if (IsLassoed)
            return;

        var aim = CalculateAim();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, aim, ShootDistance, 1 << LayerMask.NameToLayer("Player"));

        var playerDir = hit.normal;
        
        if(playerDir.x < 0 && facingRight)
        {
            FlipX();
        }
        else if(playerDir.x > 0 && !facingRight)
        {
            FlipX();
        }

        if(canFire && hit.collider !=null && hit.collider.tag.Equals("Player"))
        {
            var angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
            var zrot = new Vector3(0, 0, angle);
            var bullet = (GameObject)Instantiate(Bullet,transform.position, Quaternion.Euler(zrot));
            bullet.rigidbody2D.AddForce(aim * ShootForce);
			animator.SetTrigger("Shoot");
            StartCoroutine(ShootCooldown());
        }
    }

    private void ChasePlayer()
    {
        if (player)
        {
            var target = player.transform.position - transform.position;

            if (target.magnitude < MaxDistanceToPlayer)
                return;

            var velocity = target.normalized * Speed;
            velocity.y = 0;
            rigidbody2D.velocity = velocity;
        }
    }

    private IEnumerator SeePlayer()
    {
        seePlayer = true;
        yield return new WaitForSeconds(MemoryTimer);
        seePlayer = false;
    }

    public void NextWaypoint()
    {
            Direction = -Direction;
    }

    void OnCollisionEnter2D(Collision2D c)
    {
		Debug.Log ("____" + c.gameObject.layer);
        if(c.gameObject.tag.Equals("PlayerBullet") )
        {
            if (!Lassoed.isLassoed && !IsDead)
            {
                Debug.Log("Took Damage");
                health.TakeDamage(1);
            }

            Destroy(c.gameObject);
        }
    }

    void FlipX()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private IEnumerator ShootCooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(ShootCooldownTimer);
        canFire = true;
    }

    public void Die()
    {
        if (!isDead)
        {
			animator.SetTrigger("Died");
            Debug.Log("Died");
            score.CurrentScore += Bounty;
            isDead = true;

            OnIncapacitated();
            //DestroySelf();
        }


	}
    public void Captured()
    {
		animator.SetBool("Lassoed",true);
        OnIncapacitated();
        Debug.Log("Captured");
        score.CurrentScore += Bounty*AliveMult;
        isDead = true;
		Hashtable hash = new Hashtable();
		hash.Add("color",Color.green);
		hash.Add("oncomplete","DestroySelf");
		hash.Add("oncompletetarget",gameObject);
		hash.Add("time",0.5f);
		iTween.ColorTo(gameObject,hash);
    }

    public void DestroySelf()
    {
    	Destroy(gameObject);
    }

    private void OnIncapacitated()
    {
        if (Incapacitated != null)
        {
            Incapacitated();
        }
    }
	
	private void FlipY()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.y *= -1;
		transform.localScale = theScale;
	}
	
	public void ReverseGravity()
	{
		gravity.Reverse = !gravity.Reverse;
		FlipY();
	}


}
