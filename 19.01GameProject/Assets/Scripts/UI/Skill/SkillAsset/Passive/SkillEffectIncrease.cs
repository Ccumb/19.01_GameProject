using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SkillEffectIncrease", menuName = "Skills/Passive/SkillEffectIncreases", order = 1)]
public class SkillEffectIncrease : Skill, IUseable
{
    private Skill[] mSkills;
    public override void Use()
    {
        Debug.Log("Test CoolTimeDown");
        mSkills = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkills>().MySkills;
        SkillUse(+this.MyEffect);
        //PlayerSkill에서 스킬을 가져와서 쿨타임 다 줄여주기
    }

    public override void Relieve()
    {
        mSkills = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkills>().MySkills;
        SkillUse(-this.MyEffect);
        //PlayerSkill에서 스킬을 가져와서 쿨타임 다 늘려주기
    }

    private void SkillUse(int effect)
    {
        for(int i = 0; i < mSkills.Length; i++)
        {
            if(mSkills[i] != null)
                if(!mSkills[i].isPassive)
                    mSkills[i].MyEffect += effect;
        }
    }

    void Start()
    {
        this.isPassive = true;
    }
}
