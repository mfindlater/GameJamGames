using UnityEngine;
using System.Collections;

public class CockpitPlayer : MonoBehaviour {
    public int playerIndex;
    public float horizontalSpeed;
    public float jumpForce;
    public float gravityForce;
    public KeyCode left, right, jump;
	
	private Vector3 previousPosition;

    public static string STAND = "Stand";
    public static string RUN = "Run";
    public static string JUMP = "Jump";
	
    Sprite sprite;

    void Awake()
    {
        sprite = GetComponent<Sprite>();
        sprite.Animations.Add(STAND, new int[] { 0 });
        sprite.Animations.Add(RUN, new int[] { 1, 2, 3 });
        sprite.Animations.Add(JUMP, new int[] { 5 });
		
		previousPosition = new Vector3(rigidbody.position.x, rigidbody.position.y, rigidbody.position.z);

        if (!GameManager.ActivePlayers[playerIndex])
            this.gameObject.SetActive(false);
    }

    private Vector3 pos
    {
        get
        {
            return this.transform.position;
        }
        set
        {
            this.transform.position = value;
        }
    }

	void FixedUpdate () {
        rigidbody.AddForce(new Vector3(0, -gravityForce , 0));

        
		
		if (Input.GetKey(left))
        {
            GameManager.PlayerActions[playerIndex] += 1;
            rigidbody.velocity = new Vector3(-horizontalSpeed, rigidbody.velocity.y, rigidbody.velocity.z) ;
            sprite.FacingLeft = true;
        }
        else if (Input.GetKey(right))
        {
            GameManager.PlayerActions[playerIndex] += 1;
            rigidbody.velocity = new Vector3(horizontalSpeed, rigidbody.velocity.y, rigidbody.velocity.z) ;
            sprite.FacingLeft = false;
            
        }
		else
		{
			rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, rigidbody.velocity.z);
		}

        if (Input.GetKeyDown(jump))
        {
                GameManager.PlayerActions[playerIndex] += 1;
                rigidbody.AddForce(new Vector3(0, jumpForce, 0));
                Sounds.Instance.PlayJump();
          

        }

        if (Mathf.Abs(previousPosition.y - rigidbody.position.y) > 1)
        {
            if (sprite.CurrentAnimation != JUMP)
            {
                sprite.CurrentAnimation = JUMP;
            }
        }
        else if (Mathf.Abs(previousPosition.x - rigidbody.position.x) > 1)
        {
            if (sprite.CurrentAnimation != RUN)
            {
                sprite.CurrentAnimation = RUN;
            }
        }
        else
        {
            if (sprite.CurrentAnimation != STAND)
            {
                sprite.CurrentAnimation = STAND;
            }
        }
		
		previousPosition.x = rigidbody.position.x;
		previousPosition.y = rigidbody.position.y;
		previousPosition.z = rigidbody.position.z;
	}
}
