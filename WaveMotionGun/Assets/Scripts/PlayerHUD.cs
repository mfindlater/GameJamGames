using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour {

    public Player player;

    private Text text;

	// Use this for initialization
	void Awake ()
    {
        text = GetComponent<Text>();

        
        
	}

    void Start()
    {
        StartCoroutine(Blink());
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (player.chargePower < player.maximumCharge)
        {
            text.text = Mathf.Clamp(Mathf.RoundToInt(player.chargePower), 0, 100).ToString();
        }
        else
        {
            if (blinking)
            {
                text.text = string.Empty;
            }
            else
            {
                text.text = "READY!";
            }
        }
	}

    private bool blinking = false;

    IEnumerator Blink()
    {
        while(true)
        {
            blinking = true;
            yield return new WaitForSeconds(0.25f);
            blinking = false;
            yield return new WaitForSeconds(0.25f);
        }
    }
}
