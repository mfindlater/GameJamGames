using System.Collections;
using UnityEngine;

//Bomb
public class Bomb : MonoBehaviour {

    public Explosion explosionPrefab;
    public float timerInSeconds = 5;
    public float triggerRadius = 1f;
    public float collisionRadius = 0.7f;
    public Vector3 pickUpOffset = new Vector3(0.5f, 0.5f);
    public Vector2 direction;
    public float strength;

    private PlayerController player;
    private Rigidbody2D rigidbody2d;
    private bool isThrown = false;
    private CircleCollider2D circleCollider2D;
    private bool pickedUp = false;

    public bool InPlay { get; set; }

    private readonly string playerTag = "Player";
    private readonly string bombTag = "Bomb";
    private readonly string explosionTag = "Explosion";
    private readonly string wallTag = "Wall";
    private readonly string boundsTag = "Bounds";

    private readonly string blowUpCoroutine = "BlowUp";

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        gameObject.CreatePool(50);
    }

    void OnEnable()
    {
        StartCoroutine(blowUpCoroutine);
        circleCollider2D.isTrigger = true;
        circleCollider2D.radius = triggerRadius;
        InPlay = true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (pickedUp)
            return;

        if(collider.tag.Equals(playerTag))
        {
            player = collider.gameObject.GetComponent<PlayerController>();
            player.SetFreeBomb(this);
        }

        if(collider.tag.Equals(bombTag) || collider.tag.Equals(explosionTag))
        {
            Explode();
            if(collider.tag.Equals(bombTag))
                collider.gameObject.SendMessage("Explode");
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (pickedUp)
            return;

        if (collider.tag.Equals(playerTag))
        {
            if (player != null)
            {
                player.SetFreeBomb(null);
            }
            player = null;
            pickedUp = false;
        }
    }

    public void Pickup()
    {
        // Parent to player who picked it up.
        rigidbody2d.isKinematic = true;
        player.SetBomb(this);
        transform.SetParent(player.transform);
        pickedUp = true;
        transform.localPosition = pickUpOffset;
    }

    public void OnPlace()
    {
        // Unparent itself from player who picked it up.
        transform.SetParent(null);
        pickedUp = false;
    }

    public void Throw()
    {
        //Throw in direction player chose.
        isThrown = true;
        transform.SetParent(null);
        rigidbody2d.isKinematic = false;
        circleCollider2D.isTrigger = false;
        circleCollider2D.radius = collisionRadius;
    }

    void FixedUpdate()
    {
       if(isThrown)
        {
            rigidbody2d.MovePosition(rigidbody2d.position + direction * strength * Time.deltaTime);
        }
    }

    public IEnumerator BlowUp()
    {
        yield return new WaitForSeconds(timerInSeconds);
        Explode();
    }

    void Explode()
    {
        InPlay = false;
        StopCoroutine(blowUpCoroutine);
        explosionPrefab.Spawn(transform.position);
        gameObject.Recycle();
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(boundsTag))
        {
            var ray = Physics2D.Raycast(transform.position, direction);

            direction = Vector2.Reflect(direction, ray.normal);
        }


            if (collision.gameObject.tag.Equals(wallTag))
        {
            if(isThrown)
            {
                if(player.FacingLeft)
                {
                    if (direction.y > 0)
                    {

                        if (direction.x > 0)
                        {
                            direction = Helper.DegreeToVector2(90);
                        }
                        else if (direction.x < 0)
                        {
                            direction = Helper.RadianToVector2(-90);
                        }
                    }
                    else if (direction.y < 0)
                    {
                        if (direction.x > 0)
                        {
                            direction = Helper.DegreeToVector2(-90);
                        }
                        else if (direction.x < 0)
                        {
                            direction = Helper.RadianToVector2(90);
                        }
                    }
                }
                else
                {
                    if (direction.y < 0)
                    {

                        if (direction.x > 0)
                        {
                            direction = Helper.DegreeToVector2(45);
                        }
                        else if (direction.x < 0)
                        {
                            direction = Helper.RadianToVector2(-45);
                        }
                    }
                    else if (direction.y > 0)
                    {
                        if (direction.x > 0)
                        {
                            direction = Helper.DegreeToVector2(-45);
                        }
                        else if (direction.x < 0)
                        {
                            direction = Helper.RadianToVector2(45);
                        }
                    }
                }
            }
        }

        if(collision.gameObject.tag.Equals(playerTag) || collision.gameObject.tag.Equals(bombTag) || collision.gameObject.tag.Equals(explosionTag))
        {
            StopCoroutine(blowUpCoroutine);
            Explode();
        }
    }
}
