using UnityEngine;
using System;
using System.Collections;

public class PlatformerCharacter2D : MonoBehaviour 
{
	bool facingRight = true;							// For determining which way the player is currently facing.

	[SerializeField] float maxSpeed = 10f;				// The fastest the player can travel in the x axis.
    [SerializeField]
    Vector2 jumpForce = new Vector2(800f, 400f);			// Amount of force added when the player jumps.	
	
	[SerializeField] bool airControl = false;			// Whether or not a player can steer while jumping;
	[SerializeField] LayerMask whatIsGround;			// A mask determining what is ground to the character
    [SerializeField] LayerMask whatIsWall;
	
	Transform groundCheck;								// A position marking where to check if the player is grounded.
    Transform wallCheck;
    Transform ceilingCheck;
    float wallRadius = .1f;
	float groundedRadius = .2f;							// Radius of the overlap circle to determine if grounded
    float ceilingRadius = .1f;
	bool grounded = false;								// Whether or not the player is grounded.
    bool onWall = false;
    bool isCrushed = false;
	Animator anim;										// Reference to the player's animator component.

    public GameObject lightningParticle;
    public AudioClip lightningSound;
    //public AudioClip jumpSound;
    public AudioClip punchSound;

    float wallCooldown = 0.3f;
    bool wallJumpLock = false;

    private bool isAwake = false;

    private GameObject targetObject;
    private ScoreManager scoreManager;

    void Awake()
	{
		// Setting up references.
        wallCheck = transform.Find("WallCheck");
		groundCheck = transform.Find("GroundCheck");
        ceilingCheck = transform.Find("CeilingCheck");
		anim = GetComponent<Animator>();
        scoreManager = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreManager>();
	}


	void FixedUpdate()
	{
        if (isAwake)
        {
            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
            anim.SetBool("Ground", grounded);

            if (!wallJumpLock)
            {
                onWall = Physics2D.OverlapCircle(wallCheck.position, wallRadius, whatIsWall);
                anim.SetBool("WallClimb", onWall);
            }
            else
                onWall = false;

            // Check wall check circle for punch target
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(wallCheck.position.x - transform.position.x, 0), 1.0f, whatIsWall);
            //Debug.DrawRay(transform.position, new Vector3(wallCheck.position.x - transform.position.x, 0, -1f), Color.red);

            if (hit && hit.collider.tag == "BuildingPart")
            {
                targetObject = hit.collider.gameObject;
            }
            else
            {
                targetObject = null;
            }

            // Check if the player is being crushed by the building
            isCrushed = Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsWall);
            if (isCrushed)
            {
                SendMessage("Die");
            }

            // Set the vertical animation
            anim.SetFloat("vSpeed", rigidbody2D.velocity.y);
        }
	}

	public void Move(Vector2 move, bool jump, bool attack)
	{
        if (isAwake)
        {
            //only control the player if grounded or airControl is turned on
            if ((grounded || airControl) && !onWall)
            {
                // The Speed animator parameter is set to the absolute value of the horizontal input.
                anim.SetFloat("Speed", Mathf.Abs(move.x));

                // Move the character
                rigidbody2D.velocity = new Vector2(move.x * maxSpeed, rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move.x > 0 && !facingRight)
                    // ... flip the player.
                    Flip();
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move.x < 0 && facingRight)
                    // ... flip the player.
                    Flip();
            }

            if (onWall)
            {
                rigidbody2D.velocity = new Vector2(0, move.y * maxSpeed);
            }

            // If the player should jump...
            if ((grounded || onWall) && jump)
            {
                //audio.PlayOneShot(jumpSound);

                // Add a vertical force to the player.
                anim.SetBool("Ground", false);


                if (!onWall)
                    rigidbody2D.AddForce(new Vector2(0f, jumpForce.y));
                else
                {
                    rigidbody2D.AddForce(new Vector2(facingRight ? -jumpForce.x : jumpForce.x, jumpForce.y));
                    anim.SetBool("WallClimb", false);
                    StartCoroutine(WallCooldown());
                }
            }

            // If the character is punching
            if (attack)
            {
                audio.PlayOneShot(punchSound);
                anim.SetTrigger("Punch");
                if (targetObject != null)
                {
                    if (facingRight)
                    {
                        targetObject.GetComponent<BuildingPartController>().DamageLeft(scoreManager);
                    }
                    else
                    {
                        targetObject.GetComponent<BuildingPartController>().DamageRight(scoreManager);
                    }
                }
            }
        }
	}

    public void WakeFromStatue()
    {
        if (!isAwake)
        {
            lightningParticle.particleSystem.Play();
            lightningParticle.GetComponent<CFX_AutoDestructShuriken>().StartCoroutine("CheckIfAlive");
            audio.PlayOneShot(lightningSound);
            StartCoroutine("TransformToMan");
            isAwake = true;
        }
    }

    IEnumerator TransformToMan()
    {
        yield return new WaitForSeconds(0.1f);
        anim.SetTrigger("Awake");
    }

    IEnumerator WallCooldown()
    {
        wallJumpLock = true;
        yield return new WaitForSeconds(wallCooldown);
        wallJumpLock = false;
    }

	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
