using UnityEngine;
using InControl;

[RequireComponent(typeof(PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour 
{
	private PlatformerCharacter2D character;
    private bool jump;
    private bool punch;

	void Awake()
	{
		character = GetComponent<PlatformerCharacter2D>();
        InputManager.AttachDevice(new UnityInputDevice(new KeyboardProfile()));
	}

    void Update ()
    {
        if(InputManager.ActiveDevice.Action1.WasPressed) 
        {
            jump = true;
        }

        if(InputManager.ActiveDevice.Action2.WasPressed)
        {
            punch = true;
        }

        // Temporary until lightning effect is implemented
        if (InputManager.ActiveDevice.Action3.WasPressed)
        {
            //character.WakeFromStatue();
        }
    }

    public void Transform()
    {
        character.WakeFromStatue();
    }
	void FixedUpdate()
	{
        Vector2 move = InputManager.ActiveDevice.LeftStick;
        character.Move(move, jump, punch);
        jump = false;
        punch = false;
	}
}
