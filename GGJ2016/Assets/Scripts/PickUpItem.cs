using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private bool pickedUp = false;
    public Item item;

    public void GivePlayerItem(Player player, bool force)
    {
        if (player == null || pickedUp)
            return;

        if (player.PickUp || force)
        {
            player.SetItem(item);
            pickedUp = true;
            transform.parent.SetParent(player.transform);
            transform.parent.localPosition = new Vector3(0, 0.8f, 0);
            player.PickUp = false;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            Player player = collider.gameObject.GetComponent<Player>();
            GivePlayerItem(player,false);
        }
    }
}

