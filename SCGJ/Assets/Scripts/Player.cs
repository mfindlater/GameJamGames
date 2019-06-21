using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {
    public const int MaxBullets = 6;
    private float groundedRadius = 2f;
    private bool grounded = false;
    private Transform groundCheck;
    private Transform ceilingCheck;
    private float ceilingRadius = 2f;
    private bool facingRight = true;
    private bool invincible = false;
    public float invincibilityTimer = 1;


    private Gravity gravity;
    private Health health;


    public float MaxSpeed = 10f;
    public float JumpForce = 400f;
    public bool AirControl = false;
    public LayerMask GroundLayer;
    public float ShootForce = 400f;
    public float RecoilForce = 800f;
    public float ReloadTimer = 1f;
    public float ShootCooldownTimer = 0.15f;
    public GameObject Bullet;
    public GameObject Lasso;
    private Transform shootPosition;

    RopeSpawner ropeSpawner;

	private bool canFire = true;
	
	private Animator animator;
	
	static int DeadState = Animator.StringToHash("Base Layer.Dead");

    public Gravity Gravity
    {
        get { return gravity; }
    }

    public bool CanFire
    {
        get { return canFire; }
        set { canFire = value; }
    }

    public bool FacingRight
    {
        get { return facingRight; }
    }

    private int bulletCount = MaxBullets;

    public int BulletCount
    {
        get { return bulletCount; }
        set { bulletCount = value; }
    }

   void Awake()
   {
       groundCheck = transform.Find("GroundCheck");
       ceilingCheck = transform.Find("CeilingCheck");
       gravity = GetComponent<Gravity>();
       health = GetComponent<Health>();
       health.HealthReachedZero += Die;
       ropeSpawner = GetComponent<RopeSpawner>();
       animator = GetComponent<Animator>();
       shootPosition = transform.Find("ShootPosition");
   }

	void Update ()
	{
		AnimatorStateInfo currentBaseState = animator.GetCurrentAnimatorStateInfo(0);
		
		if (currentBaseState.nameHash == DeadState) {
			StartCoroutine("Respawn");
		}
	}

   void FixedUpdate()
   {
       grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, GroundLayer);
       gravity.Apply(rigidbody2D);
		
		

	}
	
	public void Move(float move, bool jump)
   {

       if((grounded || AirControl) && CanFire)
       {
           rigidbody2D.velocity = new Vector2(move * MaxSpeed, rigidbody2D.velocity.y);

           if (!Gravity.Reverse)
           {
               if (move > 0 && !facingRight)
               {
                   FlipX();
               }
               else if (move < 0 && facingRight)
               {
                   FlipX();
               }
           }
           else
           {
               if (move < 0 && !facingRight)
               {
                   FlipX();
               }
               else if (move > 0 && facingRight)
               {
                   FlipX();
               }
           }
       }

       if(grounded && jump)
       {
		   if (!Gravity.Reverse)
           {
               rigidbody2D.AddForce(new Vector2(0, JumpForce));
           }
           else
               rigidbody2D.AddForce(new Vector2(0, -JumpForce));
       }
		if(grounded)
		{
			animator.SetBool("Jump",false);
		} else
		{
			animator.SetBool("Jump",true);
		}
		float ReversedGravityY = (rigidbody2D.velocity.y)*-1f;;
		if (!gravity.Reverse)
		{
			animator.SetFloat("FloatVelocity", rigidbody2D.velocity.y);
		} else
		{
			animator.SetFloat("FloatVelocity", ReversedGravityY);
		}
   }

   public void Shoot(Vector2 aim)
   {
       

       StopCoroutine("Reload");
       aim.Normalize();
       if (CanFire && bulletCount > 0)
       {

          if(aim == Vector2.zero)
          {
              if (!gravity.Reverse)
              {
                  if (facingRight)
                  {
                      aim = new Vector2(1, 0);
                  }
                  else
                  {
                      aim = new Vector2(-1, 0);
                  }
              }
              else
              {
                  if (!facingRight)
                  {
                      aim = new Vector2(1, 0);
                  }
                  else
                  {
                      aim = new Vector2(-1, 0);
                  }
              }
          }

          var bullet = (GameObject)GameObject.Instantiate(Bullet);
          bullet.transform.position = shootPosition.transform.position;
          var angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
          var zrot = new Vector3(0, 0, angle);
          bullet.transform.rotation = Quaternion.Euler(zrot);

			Debug.Log(aim);
			if(aim.x == 0 && aim.y == -1)
			{
				animator.SetTrigger("ShootDown");
			} else if(aim.x == 0 && aim.y == 1)
			{
				animator.SetTrigger("ShootUp");
			} else if(aim.x != 0 && aim.y < 0)
			{
				animator.SetTrigger("ShootDownRight");
			} else if(aim.x != 0 && aim.y > 0)
			{
				animator.SetTrigger("ShootUpRight");
			} else 
			{
				animator.SetTrigger("ShootRight");
			}
          if (!gravity.Reverse)
          {
              bullet.rigidbody2D.AddForce(aim * ShootForce);
          }
          else
          {
              bullet.rigidbody2D.AddForce(aim * ShootForce);
          }

          if (gravity.GravityState != GravityState.Normal)
          {
              rigidbody2D.AddRelativeForce(-aim * RecoilForce);
          }

          StartCoroutine(ShootCooldown());
       }
       else if(CanFire && bulletCount == 0)
       {
           StartCoroutine("Reload");
       }

   }


   public void ThrowLasso(Vector2 direction)
   {
		direction.Normalize();
		if(direction == Vector2.zero)
		{
			if (!gravity.Reverse)
			{
				if (facingRight)
				{
					direction = new Vector2(1, 0);
				}
				else
				{
					direction = new Vector2(-1, 0);
				}
			}
			else
			{
				if (!facingRight)
				{
					direction = new Vector2(1, 0);
				}
				else
				{
					direction = new Vector2(-1, 0);
				}
			}

		}
		ropeSpawner.FireRope(direction);
	}
		
		void OnDrawGizmos()
		{
			if(ceilingCheck) Gizmos.DrawSphere(ceilingCheck.position, ceilingRadius);
			if(groundCheck) Gizmos.DrawSphere(groundCheck.position, groundedRadius);
		}
		
		public IEnumerator Invincibility()
		{
			invincible = true;
			yield return new WaitForSeconds(invincibilityTimer);
       invincible = false;
   }

   public void StartReload()
   {
       StopCoroutine("Reload");
       StartCoroutine("Reload");
   }

   public void StopReload()
   {
       StopCoroutine("Reload");
   }

   private IEnumerator Reload()
   {
       while (BulletCount < MaxBullets)
       {
           yield return new WaitForSeconds(ReloadTimer/MaxBullets);
           BulletCount++;
       }
   }

   private IEnumerator ShootCooldown()
   {
       CanFire = false;
       bulletCount--;
       yield return new WaitForSeconds(ShootCooldownTimer);
       CanFire = true;
   }

   private void FlipX()
   {
       facingRight = !facingRight;

       Vector3 theScale = transform.localScale;
       theScale.x *= -1;
       transform.localScale = theScale;
   }

   private void FlipY()
   {
       facingRight = !facingRight;
       Vector3 theScale = transform.localScale;
       theScale.y *= -1;
       transform.localScale = theScale;
   }

   public void DestroyLasso()
   {
      if (ropeSpawner.RopeFired)
       {
           ropeSpawner.destroyTheRope();
       }
   }

   void Die()
   {
		invincible = true;
		animator.SetTrigger("Dead");
   }
	
	IEnumerator Respawn()
	{
		animator.SetTrigger("Respawn");
		DestroyLasso();
       BulletCount = MaxBullets;
       gravity.GravityState = GravityState.Normal;
       rigidbody2D.velocity = Vector2.zero;
       if(GameWorld.Checkpoint)
           transform.position = GameWorld.Checkpoint.transform.position;
       health.Reset();
       yield return StartCoroutine(Invincibility());
   }

   void OnCollisionEnter2D(Collision2D c)
   {
       if(c.gameObject.tag.Equals("Enemy"))
       {
           if (!invincible && !c.gameObject.GetComponent<Enemy>().IsDead)
           {
				animator.SetTrigger("Hit");
               health.TakeDamage(1);
               StartCoroutine(Invincibility());
           } 
       }
       
       if (c.gameObject.tag.Equals("EnemyBullet"))
       {
           Destroy(c.gameObject);
           
           if (!invincible)
           {
				animator.SetTrigger("Hit");
               health.TakeDamage(1);
               StartCoroutine(Invincibility());
           }
       }
   }

   public void ReverseGravity()
   {
       gravity.Reverse = !gravity.Reverse;
       FlipY();
   }
}
