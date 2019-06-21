using UnityEngine;

public class SetPlayerContext : MonoBehaviour
{
    public ItemType RequiredItemType = ItemType.None;
    public PlayerAction Context;
    public bool LastContextOnExit = false;
    private PlayerAction lastContext;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            Player player = collider.gameObject.GetComponent<Player>();
            if (player)
            {
                Item item = player.GetItem();

                

                bool changeContext = ((item.ItemType == RequiredItemType || (Context == PlayerAction.ExitRoom || Context == PlayerAction.ExitStore)));//|| 

                if (changeContext)
                {
                    lastContext = player.ContextAction;
                    player.ContextAction = Context;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            Player player = collider.gameObject.GetComponent<Player>();
            if (player)
            {
                if(LastContextOnExit)
                    player.ContextAction = lastContext;
                player.ContextAction = PlayerAction.None;
            }
        }
    }
}
