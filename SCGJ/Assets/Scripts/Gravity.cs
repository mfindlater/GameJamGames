using UnityEngine;
using System.Collections;

public enum GravityState
{
    Zero,
    Low,
    Normal
};

public class Gravity : MonoBehaviour
{

    private Vector2 currentGravity;
    private Vector2 lowGrav = new Vector2(0, -250f);
    private Vector2 normalGrav = new Vector2(0, -500f);
    private GravityState gravityState = GravityState.Normal;


    public GravityState GravityState
    {
        get { return gravityState; }
        set { SetGravity(value); }
    }

    public bool Reverse { get; set; }

    void Start()
    {
        SetGravity(GravityState.Normal);
        Reverse = false;
    }

    public Vector2 Current
    {
        get
        {

            if (Reverse)
                return currentGravity * -1;
            else
                return currentGravity;
        }
        set { currentGravity = value; }
    }

    public void Apply(Rigidbody2D r)
    {
        r.AddForce(Current);
    }

    private void SetGravity(GravityState gstate)
    {
        gravityState = gstate;

        switch (GravityState)
        {
            case GravityState.Low:
                Current = lowGrav;
                break;
            case GravityState.Normal:
                Current = normalGrav;
                break;
            default:
                Current = Vector2.zero;
                break;
        }
    }
}
