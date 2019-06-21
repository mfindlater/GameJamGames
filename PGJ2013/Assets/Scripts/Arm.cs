using UnityEngine;
using System.Collections;

public class Arm : MonoBehaviour {
    private bool punching = false;
    private bool retracting = false;
    private float secondsForAction;
    private Quaternion punchOrigin;
    private float originRotZ;
    public Robot parent { get; set; }
    private int mult { get { return parent.leftFacing ? -1 : 1; } }
    Fist[] fists;
    void Start()
    {
        fists = GetComponentsInChildren<Fist>();
    }

    public void Punch()
    {
        if(punching || retracting)
        {
            return;
        }

        punching = true;
        punchOrigin = transform.rotation;
    }

    void Update()
    {
        if (punching)
        {
            transform.Rotate(Vector3.forward, 
                Time.deltaTime / Def.Instance.secondsToPunch * Def.Instance.punchRotation * mult);
            secondsForAction += Time.deltaTime;
            if (secondsForAction > Def.Instance.secondsToPunch)
            {
                //swing complete -- begin retract
                secondsForAction = 0;
                retracting = true;
                punching = false;
            }
        }
        else if (retracting)
        {
            transform.Rotate(Vector3.forward,
                Time.deltaTime / Def.Instance.secondsToPunch * -Def.Instance.punchRotation * mult);
            secondsForAction += Time.deltaTime;
            if (secondsForAction > Def.Instance.secondsToPunch)
            {
                //return to origin state
                secondsForAction = 0;
                retracting = false;
                transform.rotation = punchOrigin;
            }
        }
        else
        {
            if(fists != null && fists.Length > 0)
            {
                fists[0].rigidbody.velocity = Vector3.zero;
            //fists[0].transform.position = VectorUtil.Add(transform.position, 182, -3.5f, -31);
            fists[0].transform.localPosition = new Vector3(0, -0.5f, -10);
            }
        }
    }
}
