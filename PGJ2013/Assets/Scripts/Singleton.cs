using UnityEngine;
using System;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T backingInstance;

    /**
       Returns the instance of this singleton.
    */
    public static T Instance
    {
        get
        {
            if (backingInstance == null)
            {
                backingInstance = (T)FindObjectOfType(typeof(T));

                if (backingInstance == null)
                {
                    Debug.LogError("An instance of " + typeof(T) +
                       " is needed in the scene, but there is none.");
                }
            }

            return backingInstance;
        }
    }

    public static bool Exists()
    {
        return (T)FindObjectOfType(typeof(T)) != null;
    }
}