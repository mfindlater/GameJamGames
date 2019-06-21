using UnityEngine;
using System.Collections;

public class MoveButton : MonoBehaviour {
    public float upY;
    public float downY;
    public bool left;
    private int collisionCount;
    public Robot bodyToMove;
	private Vector3 initialPosition;
    private bool pressed = false;
    public float moveAmount;
	
	private const float downOffset = -5.0f;

    void Start()
    {
		initialPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    public void Press()
    {
        if (collisionCount == 0)
        {
            pressed = true;
            transform.position = new Vector3(initialPosition.x, initialPosition.y + downOffset, initialPosition.z);
            Sounds.Instance.PlayButtonPress();
        }
		
        collisionCount++;
    }

    public void Unpress()
    {
        collisionCount--;
        if (collisionCount == 0)
        {
            pressed = false;
            transform.position = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z);
        }
    }

    public void FixedUpdate()
    {
        if (pressed)
        {
            GameObject[] robots = GameObject.FindGameObjectsWithTag("Robot");
            float robotX0 = robots[0].GetComponent<Robot>().transform.position.x;
            float robotX1 = robots[1].GetComponent<Robot>().transform.position.x;
            if (Mathf.Abs(robotX0 - robotX1) < 45
                && ((bodyToMove.leftFacing && left)
                || (!bodyToMove.leftFacing && !left)))
            {
                //too close and moving closer
                return;
            }
            if ((Camera.main.WorldToViewportPoint(bodyToMove.transform.position).x < .8f && left)  || (Camera.main.WorldToViewportPoint(bodyToMove.transform.position).x > 1.5f && !left))
            {
                return;
            }

            bodyToMove.transform.position = VectorUtil.Add(bodyToMove.transform.position, x: (left ? -1 : 1) * moveAmount);
        }
    }
}
