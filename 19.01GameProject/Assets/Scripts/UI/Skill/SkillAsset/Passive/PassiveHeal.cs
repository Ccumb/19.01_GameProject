using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveHeal", menuName = "Skills/Passive/PassiveHeal", order = 1)]
public class PassiveHeal : Skill, IUseable
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
