using UnityEngine;
using Rewired;
public enum PlayerState
{
    Normal,
    Pickup,
    Throw
}

public class PlayerController : MonoBehaviour
{
    public int playerId = 0;
    public int speed = 8;
    public float aimSmooth = 5;
    public AudioClip pickupSound, throwSound;
    public bool isAlive = false;
    public bool FacingLeft = true;
    public float throwStrengthMax = 10;
    public float throwStrengthGrowRate = 0.1f;
    public float aimLineLength = 8;
    private Player player;
    private Vector3 move;
    private bool pickupOrThrowPressed;
    private Bomb freeBomb;
    private Bomb bomb;
    private Rigidbody2D rigidbody2d;
    private Vector3 throwDirection;
    private float throwStrength;
    private Vector2[] throwingDirections;
    private int throwingDirectionIndex = 0;
    private LineRenderer lineRenderer;
    private bool initialized = false;
    private Animator animator;
    private AudioSource audioSource;
    private bool throwPressed = false;
    private bool throwReleased = false;
    private PlayerState playerState; 

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        isAlive = true;
        throwingDirections = new Vector2[11];

        throwingDirections[0] = Helper.DegreeToVector2(75);
        throwingDirections[1] = Helper.DegreeToVector2(60);
        throwingDirections[2] = Helper.DegreeToVector2(45);
        throwingDirections[3] = Helper.DegreeToVector2(30);
        throwingDirections[4] = Helper.DegreeToVector2(15);
        throwingDirections[5] = Helper.DegreeToVector2(0);
        throwingDirections[6] = Helper.DegreeToVector2(-15);
        throwingDirections[7] = Helper.DegreeToVector2(-30);
        throwingDirections[8] = Helper.DegreeToVector2(-45);
        throwingDirections[9] = Helper.DegreeToVector2(-60);
        throwingDirections[10] = Helper.DegreeToVector2(-75);
    }

    private void Initialize()
    {
        player = ReInput.players.GetPlayer(playerId);
        initialized = true;
    }

    void OnEnable()
    {
        isAlive = true;
        playerState = PlayerState.Normal;
    }
 
    void UpdateInput()
    {
        if (!initialized) Initialize();

        pickupOrThrowPressed = player.GetButtonDown("PickUp/Throw");
        throwPressed = player.GetButton("PickUp/Throw");
        throwReleased = player.GetButtonUp("PickUp/Throw");
        

        if (playerState == PlayerState.Normal || playerState == PlayerState.Pickup)
        {
            float x = speed * player.GetAxis("MoveX") * Time.deltaTime;
            float y = speed * player.GetAxis("MoveY") * Time.deltaTime;
            move = new Vector3(x, y);
            move = Vector3.ClampMagnitude(move, speed * Time.deltaTime);
        }

        if(playerState == PlayerState.Pickup && pickupOrThrowPressed)
        {
            throwStrength = 0;
            throwingDirectionIndex = Mathf.FloorToInt(throwingDirections.Length / 2);
            ChangeState(PlayerState.Throw);
        }

        if(playerState == PlayerState.Throw && throwPressed)
        {
            if (player.GetAxis("MoveY") < 0)
            {
                throwingDirectionIndex = (FacingLeft) ? (int)Mathf.Lerp(throwingDirectionIndex, 0, aimSmooth) : (int)Mathf.Lerp(throwingDirectionIndex, throwingDirections.Length, aimSmooth);
            }

            if (player.GetAxis("MoveY") > 0)
            {
                throwingDirectionIndex = (FacingLeft) ? (int)Mathf.Lerp(throwingDirectionIndex, throwingDirections.Length, aimSmooth) : (int)Mathf.Lerp(throwingDirectionIndex, 0, aimSmooth);
            }

            throwingDirectionIndex = Mathf.Clamp(throwingDirectionIndex, 0, throwingDirections.Length - 1);

            var throwDir = (FacingLeft) ? -throwingDirections[throwingDirectionIndex] : throwingDirections[throwingDirectionIndex];
            var ray = new Ray2D(transform.position, throwDir);

            Vector3[] vertices = new Vector3[]
            {
                transform.position,
                ray.GetPoint(aimLineLength * (throwStrength/throwStrengthMax))
            };

            lineRenderer.SetPositions(vertices);
            lineRenderer.enabled = true;
        }
        else if(!throwPressed)
        {
            lineRenderer.enabled = false;
            lineRenderer.SetPositions(new Vector3[0]);
        }
    }

    void FixedUpdate()
    {
        if(playerState == PlayerState.Throw)
            return;

        Vector2 velocity = new Vector2(move.x, move.y);
        rigidbody2d.MovePosition(rigidbody2d.position + velocity);
    }

    void Update()
    {
#if DEBUG
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Die();
        }
#endif

        UpdateInput();

        animator.SetBool("Moving", move.magnitude > 0.1f);

        if (playerState == PlayerState.Normal)
        {
            if (!HaveBomb && IsBombAvailable && pickupOrThrowPressed)
            {
                freeBomb.Pickup();
                audioSource.PlayOneShot(pickupSound, 1);
                ChangeState(PlayerState.Pickup);
                SetFreeBomb(null);
            }
        }

        if (playerState == PlayerState.Throw)
        {
            throwStrength = Mathf.Lerp(throwStrength, throwStrengthMax, throwStrengthGrowRate * Time.deltaTime);

            if (throwReleased && HaveBomb)
            {
                throwingDirectionIndex = Mathf.Clamp(throwingDirectionIndex, 0, throwingDirections.Length - 1);

                bomb.direction = (FacingLeft) ? -throwingDirections[throwingDirectionIndex] : throwingDirections[throwingDirectionIndex];
                bomb.strength = throwStrength;
                bomb.Throw();
                audioSource.PlayOneShot(throwSound, 1);
                SetBomb(null);
                ChangeState(PlayerState.Normal);
            }
        }
    }

    public void ChangeState(PlayerState state)
    {
        //Debug.Log(string.Format("{0} > {1}", playerState, state));
        playerState = state;
    }

    public void SetBomb(Bomb b)
    {
        bomb = b;
    }

    public bool IsBombAvailable
    {
        get { return freeBomb != null; }
    }

    public bool HaveBomb
    {
        get { return bomb != null; }
    }

    public void SetFreeBomb(Bomb bomb)
    {
        freeBomb = bomb;
    }

    public void Die()
    {
        isAlive = false;
        gameObject.Recycle();
    }
}
