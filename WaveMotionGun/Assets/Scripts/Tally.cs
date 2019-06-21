using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tally : MonoBehaviour, ITallyListener {

    private Text text;
    private string tallyText;

    public void OnTally(int tally)
    {
        StopCoroutine("SetTallyText");
        StartCoroutine("SetTallyText", tally);
    }

    IEnumerator SetTallyText(int tally)
    {
        tallyText = string.Format("LINK {0}", tally);
        yield return new WaitForSeconds(0.75f);
        tallyText = string.Empty;
    }

    void Awake()
    {
        ScoreManager.Listeners.Add(this);
        text = GetComponent<Text>();
    }

	// Update is called once per frame
	void Update ()
    {
        text.text = tallyText;
	}
}
