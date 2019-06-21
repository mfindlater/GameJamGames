using System;
using UnityEngine;

[Serializable]
public class FlashlightState
{
    public bool On { get; private set; }
    
    public float BatteryPecentage;
    public float BatteryLifeSeconds = 60;

    public FlashlightState()
    {
        BatteryPecentage = 100;
		On = false;
    }

    public bool ToggleSwitch()
    {
        bool lastState = On;
        On = !On;
        return lastState == false && On;
    }

    public void Update()
    {
        if(On)
        {
            BatteryPecentage -= Time.deltaTime * 100 / BatteryLifeSeconds;
        }

        BatteryPecentage = Mathf.Clamp(BatteryPecentage, 0, 100);

        if (BatteryPecentage == 0) On = false;
    }
}