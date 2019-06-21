using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


    public static class GameWorld
    {
        public static Player Player { get; set; }
        public static GameObject Checkpoint { get; set; }
        public static bool Paused {get; set;}
        public static bool GameOver {get; set;}
    }

