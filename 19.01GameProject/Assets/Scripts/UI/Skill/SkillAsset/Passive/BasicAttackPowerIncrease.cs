using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicAttackPowerIncrease", menuName = "Skills/Passive/BasickAttackPowerIncrease", order = 1)]
public class BasicAttackPowerIncrease : Skill, IUseable
{
    public override void Use()
    {

    }
    public override void Relieve()
    {
        
    }
    void Start()
    {
        this.isPassive = true;
    }
}
