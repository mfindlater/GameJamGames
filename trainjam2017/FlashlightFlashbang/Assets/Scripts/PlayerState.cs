using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


[Serializable]
public class PlayerState
{
    public int PlayerId { get; set; }
    public bool IsReady { get; set; }
    public bool IsDead { get; set; }

    public FlashlightState Flashlight { get; set; }
}

