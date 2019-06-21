using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class BattleText : MonoBehaviour {

    private Text text;
    private bool showText = false;

    public void SayText(string sayText, float seconds)
    {
        StartCoroutine(Say(sayText, seconds));
    }

    private IEnumerator Say(string say, float seconds)
    {
        showText = true;
        text.text = say;
        yield return new WaitForSeconds(seconds);
        text.text = string.Empty;
        showText = false;
        yield return null;
    }

	void Awake ()
    {
        text = GetComponent<Text>();
	}

    void OnEnable()
    {
        StartCoroutine(Say("Game Appeared!",2));
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

	void Update ()
    {
        if(!showText)
        {
            if(text.text.Length > 0)
                text.text = string.Empty;
        }
	}
}
