using UnityEngine;
using System.Collections;

public class PunchButton : MonoBehaviour {
    public bool pressed;
    public PunchButton counterButton;
    private int collisionCount;
    private bool unpressing;
    public PunchButtonTrigger parent;
    public Robot robot;
	
	private Vector3 initialPosition;
	private const float downOffset = -5.0f;

    void Start()
    {
		initialPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		
		if(pressed)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + downOffset, transform.position.z);
		}
    }

    public void Flip()
    {
        if (!pressed)
        {
            pressed = true;
            transform.position = new Vector3(initialPosition.x, initialPosition.y + downOffset, initialPosition.z);
            Sounds.Instance.PlayButtonPress();
        }
        else if (pressed && !parent.HasColliders())
        {
            pressed = false;
            transform.position = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z);
        }
    }

    public void Press()
    {
        if (collisionCount == 0 && !pressed)
        {
            MakeRobotPunch();
            Flip();
            counterButton.Flip();
        }
        collisionCount++;
    }

    public void Unpress()
    {
        collisionCount--;
        if (pressed && counterButton.pressed)
        {
            //unpress only if neither was unpressed
            Flip();
        }
    }

    private void MakeRobotPunch()
    {
        robot.Punch();
    }
}
