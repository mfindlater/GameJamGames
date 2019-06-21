using UnityEngine;
using System.Collections;
using System;

public class Robot : MonoBehaviour {
    Sprite sprite;
    public bool leftFacing;
    private Arm arm;
    void Start()
    {
        arm = GetComponentInChildren<Arm>();
        arm.parent = this;
    }

    public void Punch()
    {
        arm.Punch();
    }

    public void RegisterHit()
    {
        throw new Exception("blapoo");
    }
}
