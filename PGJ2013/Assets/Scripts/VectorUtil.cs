using UnityEngine;
using System.Collections;

public static class VectorUtil {
    public static Vector3 Add(Vector3 vec, float x = 0, float y = 0, float z = 0)
    {
        return new Vector3(vec.x + x, vec.y + y, vec.z + z);
    }
}
