using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public float timeLeft = 0f;
    public bool timeActive = false;
	public Text text;

    //max time is 99.
    public string[] numbers = new string[100];

    void Awake()
    {
        for(int i=0; i < numbers.Length; i ++)
        {
            numbers[i] = i.ToString();
        }
    }
	
	// Update is called once per frame
	void Update()
    {
        if (!timeActive)
            return;

        timeLeft -= Time.deltaTime;
        int numIndex = Mathf.Clamp(Mathf.RoundToInt(timeLeft), 0, 100);
        text.text = numbers[numIndex];
	}
}
