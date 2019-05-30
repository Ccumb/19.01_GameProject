using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;

[CreateAssetMenu(fileName = "BasicAttackPowerIncrease", menuName = "Skills/Passive/BasickAttackPowerIncrease", order = 1)]
public class BasicAttackPowerIncrease : Skill, IUseable
{
    public override void Use()
    {        
        EventManager.TriggerIntEvent("IncreasedAttackDamage", MyEffect);
    }
    public override void Relieve()
    {
        EventManager.TriggerIntEvent("IncreasedAttackDamage", -MyEffect);
    }
}
