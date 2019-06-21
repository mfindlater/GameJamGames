using System.Collections.Generic;
using System;
using UnityEngine;


public enum SkillResult
{
    Success,
    Failed
}

public enum SkillName
{
    LeanIn,
    Grind,
    Mash,
    BePatient
}

public enum SkillType
{
    Delayed,
    Recurring,
    Buff,
    Debuff,
    Instant
}

[CreateAssetMenu]
[Serializable]
public class Skill : ScriptableObject
{
    public SkillName SkillName;
    public SkillType SkillType;
    public int Cost;
    public int Amount;

    public string Use(Combatant user, Combatant target)
    {
        int damage = 0;
        switch(SkillName)
        {
            case SkillName.Mash:
                damage = DamageCalculation(user, target, Amount);
                user.Act -= Cost;
                return string.Format("Player used Mash, did [{0}] damage!",damage);
            case SkillName.LeanIn:
                user.Act -= Cost;
                user.Act += Amount;
                return string.Format("Player used LeanIn,  +[{0}] speed!", Amount);
            case SkillName.Grind:
                damage = DamageCalculation(user, target, Amount);
                user.Act -= Cost;
                return string.Format("Player used Grind, did [{0}] damage!", damage);
        }

        return string.Empty;
    }

    private static int DamageCalculation(Combatant user, Combatant target, int damage)
    {
        if(target.Health - damage >= 0)
        {
            target.Health -= damage;
            return damage;
        }
        else if (target.Health - damage < 0)
        {
            target.Health = 0;
            int d = damage - target.Health;
            return d;
        }
        return 0;
    }


}




