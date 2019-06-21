using UnityEngine;
using System;
using System.Collections;

public class Health : MonoBehaviour {

    public float currentHealth = 10;
    public float MaxHealth = 10;
    public float x = 10;
    public float y = 10;
    public Color foregroundColor = Color.yellow;
    public Color backgroundColor = Color.red;
    public Action TookDamage;



    Rect box = new Rect(10, 10, 128, 20);

    private Texture2D background;
    private Texture2D foreground;

    public Action Dead;

    protected void OnDead()
    {
        if (Dead != null)
            Dead();

        Application.LoadLevel("results");
    }

    public void OnGUI()
    {

        GUI.BeginGroup(box);
        {
            GUI.DrawTexture(new Rect(0, 0, box.width, box.height), background, ScaleMode.StretchToFill);
            GUI.DrawTexture(new Rect(0, 0, box.width * currentHealth / MaxHealth, box.height), foreground, ScaleMode.StretchToFill);
        }
        GUI.EndGroup(); 
    }


    public void TakeDamage(int damage)
    {

        if (TookDamage != null)
            TookDamage();

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            OnDead();
            currentHealth = 0;
        }
    }

	// Use this for initialization
	void Start () {
        currentHealth = MaxHealth;

        box = new Rect(x, y, 300, 10);

        background = new Texture2D(1, 1, TextureFormat.RGB24, false);
        foreground = new Texture2D(1, 1, TextureFormat.RGB24, false);

        background.SetPixel(0, 0, backgroundColor);
        foreground.SetPixel(0, 0, foregroundColor);

        background.Apply();
        foreground.Apply();
	
	}
	
	// Update is called once per frame
	void Update () {

        if (currentHealth < 0)
            currentHealth = 0;

        if (currentHealth > MaxHealth)
            currentHealth = MaxHealth;
	
	}
}
