using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public static class VectorExtensions
{
    public static Vector2 AsVector2(this Vector3 v)
    {
        return new Vector2(v.x, v.y);
    }

    public static Vector3 AsVector3(this Vector2 v)
    {
        return new Vector3(v.x, v.y);
    }
}

