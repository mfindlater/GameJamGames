using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayContextAction : MonoBehaviour
{
    private Text text;
    public Player Player;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        switch(Player.ContextAction)
        {
            case PlayerAction.PickUp:
                text.text = "ACTION:PICK UP";
                break;
            case PlayerAction.Play:
                text.text = "ACTION:PLAY";
                break;
            case PlayerAction.Drop:
                text.text = "ACTION:DROP";
                break;
            case PlayerAction.Throw:
                text.text = "ACTION:THROW";
                break;
            case PlayerAction.ExitStore:
            case PlayerAction.ExitRoom:
                text.text = "ACTION:EXIT";
                break;
            default:
                text.text = string.Empty;
                break;
        }
    }
}
