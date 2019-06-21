using UnityEngine;
using System.Collections;
using System;

public enum ItemType
{
    Cartridge,
    Console,
    TV,
    None
}

[Serializable]
public class Item
{
    public static Item Empty = new Item();
    public ItemType ItemType = ItemType.None;
    public string Name = string.Empty;
}
