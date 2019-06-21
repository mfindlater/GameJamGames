using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayItem : MonoBehaviour {

    public Player player;

    private Text text;

	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = string.Format("Item:{0}", player.GetItem().ItemType.ToString());
	}
}
