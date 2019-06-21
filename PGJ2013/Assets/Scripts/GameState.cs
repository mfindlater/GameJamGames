using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

    public static Menu.GameState State
    {
        get
        {
            if (Menu.Exists())
            {
                return Menu.Instance.CurrentState;
            }
            if (FindObjectsOfType(typeof(Robot)).Length != 0)
            {
                return Menu.GameState.Game;
            }
            return Menu.GameState.GameResults;
        }
    }
}
