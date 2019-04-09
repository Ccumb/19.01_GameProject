using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CoolTimeDownSkill", menuName = "PassiveSkills/CoolTimeDown", order = 1)]
public class PCoolTimeDownSkill : Skill, IUseable
{
    public int reduceCoolTime = 2;
    public override void Use()
    {
        //PlayerSkill에서 스킬을 가져와서 쿨타임 다 줄여주기
    }

    public override void Relieve()
    {
        //PlayerSkill에서 스킬을 가져와서 쿨타임 다 늘려주기
    }

    void Start()
    {
        this.isPassive = true;
    }
}
