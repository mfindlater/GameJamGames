using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Combatant
{
    [HideInInspector]
    public int Health;
    [HideInInspector]
    public float Act;
    public string Name;
    public const float Ready = 100;
    public Stats Stats = new Stats();
    public List<Skill> Skills = new List<Skill>();

    public void Initialize()
    {
        Health = Stats.MaximumHealth;
        Act = 0;
    }
}
