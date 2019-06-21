using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : Pickup {

    public float Percentage = 25;

    public override void OnPickup(PlayerBehavior player)
    {
        player.flashLightState.BatteryPecentage += Percentage;
        base.OnPickup(player);
    }
}
