using System;
using UnityEngine;

public enum PlayerAction
{
    Play,
    PickUp,
    Drop,
    Throw,
    ExitRoom,
    ExitStore,
    None
}

public class Player : MonoBehaviour {

    public float Speed = 10;
    private Vector2 move;
    private Rigidbody2D rBody2d;

    private Item item;
   
    public Item GetItem()
    {
        if (item == null)
            return Item.Empty;

        return item;
    } 
    
    public void SetItem(Item item)
    {
        animator.SetTrigger("PickUp");
        this.item = item;

    } 

    private GameManager gameManager;

    public PlayerAction ContextAction = PlayerAction.None;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public bool PickUp { get; set; }

    void Awake () {
        rBody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	void Update ()
    {
        move = new Vector2(Input.GetAxis("Horizontal"),0).normalized;

        animator.SetBool("Moving", move.x > 0 || move.x < 0);

        if (move.x > 0)
        {
            spriteRenderer.flipX = false;   
        }
        else if(move.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        animator.SetBool("Carrying", GetItem().ItemType != ItemType.None);

        bool action = Input.GetKeyDown(KeyCode.Space);

        if(action)
        {
            switch(ContextAction)
            {
                case PlayerAction.PickUp:
                    PickUp = true;
                    break;
                case PlayerAction.Throw:
                    break;
                case PlayerAction.Play:
                    gameManager.StartBattle();
                    break;
                case PlayerAction.ExitRoom:
                    gameManager.EnterStore(gameObject);
                    break;
                case PlayerAction.ExitStore:
                    gameManager.EnterRoom(gameObject);
                    break;
                default:
                    break;
            }
        }
	}

    internal void ClearItem()
    {
        SetItem(null);

        var p = GetComponentInChildren<PickUpItem>();
        if (p)
        {
            Destroy(p.gameObject.transform.parent.gameObject);
        }
    }

    void FixedUpdate()
    {
        if (move.magnitude > 0.0001f)
        {
            rBody2d.MovePosition(rBody2d.position + (move * Speed) * Time.fixedDeltaTime);
        }
        else
        {
            rBody2d.velocity = Vector2.zero;
        }
    }
}
