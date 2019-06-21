using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

    public int score = 0;
    public static Score instance;

    void Start()
    {
        instance = this;
    }
	
}
