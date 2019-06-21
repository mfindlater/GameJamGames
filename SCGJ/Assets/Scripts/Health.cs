using UnityEngine;
using System.Collections;
using System;

public class Health : MonoBehaviour {

    public float CurrentHealth;
    public float MaxHealth = 1;
    public Action HealthReachedZero;
    public Action OnDameged;
    public bool HealthFlash = true;
    private Color baseColor;
    public Color FlashColor = new Color(1f,0f,0f,1f);

    public void Reset()
    {
        CurrentHealth = MaxHealth;
    }

	// Use this for initialization
	void Start () {
        Reset();
        baseColor = renderer.material.GetColor("_Color");
	}

    public void TakeDamage(float amount)
    {
        if (OnDameged != null)
            OnDameged();
        CurrentHealth -= amount;
        if (HealthFlash)
        {
            FlashRed();
        }
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;

            if (HealthReachedZero != null)
                HealthReachedZero();
        }
    }
    void FlashRed()
    {
        renderer.material.SetColor("_Color",baseColor);
        iTween.ColorFrom(gameObject,FlashColor,0.4f);
    }
}
