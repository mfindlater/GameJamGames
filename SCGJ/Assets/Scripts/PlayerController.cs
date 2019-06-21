using UnityEngine;
using System.Collections;
using InControl;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour {

    Player player;

    private bool shoot = false;
    Vector2 aim = Vector2.zero;
    private bool jump = false;
    private bool lasso = false;

    InputDevice input;

	private Animator animator;
	
	private int DeathStateHash = Animator.StringToHash("Base Layer.Death");

	// Use this for initialization
	void Awake() 
    {
        player = GetComponent<Player>();
        InputManager.Setup();
        input = InputManager.ActiveDevice;
        Debug.Log(input.Name);
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameWorld.GameOver) return;
		AnimatorStateInfo stateInfo =  animator.GetCurrentAnimatorStateInfo(0);
		if(stateInfo.nameHash != DeathStateHash)
		{
			InputManager.Update();
			if(Input.GetKeyDown(KeyCode.X) || input.Action1.WasPressed)
			{
				jump = true;
			}
			
			if(Input.GetKeyDown(KeyCode.Z) || input.Action3.WasPressed)
			{
				shoot = true;
			}
			
			if(Input.GetKeyDown(KeyCode.C) || input.Action4.WasPressed)
			{
				lasso = true;
				animator.SetTrigger("Lasso");
			}
			
			if (Input.GetKeyDown(KeyCode.R) || input.RightBumper)
			{
				player.StartReload();
			}
			
			//Debug Keybindings
			
			
			
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				player.Gravity.GravityState = GravityState.Normal;
			}
			
			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				player.Gravity.GravityState = GravityState.Low;
			}
			
			if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				player.Gravity.GravityState = GravityState.Zero;
			}
			
			if(Input.GetKeyDown(KeyCode.Alpha4))
			{
				player.ReverseGravity();
			}
			
			
			
			
			//aim = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			
			Vector2 direction = GetDirection();
			
			if(direction == Vector2.zero)
			{
				aim = new Vector2(input.LeftStick.X, input.LeftStick.Y);
			}
			else
			{
				aim = direction;
			}
		}
		
	}
	
	Vector2 GetDirection()
	{
		Vector2 dir = Vector2.zero;
		
		if(Input.GetKey(KeyCode.UpArrow))
		{
			dir.y += 1;
		}
		
		if(Input.GetKey(KeyCode.DownArrow))
		{
			dir.y -= 1;
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            dir.x -= 1;
        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            dir.x += 1;
        }
        return dir;
    }

    void FixedUpdate()
    {
        if (GameWorld.GameOver) return;
		AnimatorStateInfo stateInfo =  animator.GetCurrentAnimatorStateInfo(0);
		if(stateInfo.nameHash != DeathStateHash)
		{
			float horizontal = 0;
			if(Input.GetKey(KeyCode.LeftArrow))
			{
				horizontal = -1;
			}
			
			if(Input.GetKey(KeyCode.RightArrow))
			{
				horizontal = 1;
			}
			
			if (horizontal == 0)
			{
				horizontal = input.LeftStick.X; 
			}
			
			if (horizontal != 0)
			{
				animator.SetBool("Run", true);
			}
			else
				animator.SetBool("Run", false);
			
			float h = horizontal;
			
			player.Move(h, jump);
			
			if(shoot)
			{
				player.Shoot(aim);
			}
			
			if(lasso)
			{
				player.ThrowLasso(aim);
			}
			
			jump = false;
			shoot = false;
			lasso = false;
		}		
	}
}
