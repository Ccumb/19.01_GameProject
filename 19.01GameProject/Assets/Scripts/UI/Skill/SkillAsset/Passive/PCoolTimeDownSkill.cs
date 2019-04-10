using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CoolTimeDownSkill", menuName = "Skills/Passive/CoolTimeDown", order = 1)]
public class PCoolTimeDownSkill : Skill, IUseable
{
    private Skill[] mSkills;
    public override void Use()
    {
        Debug.Log("Test CoolTimeDown");
        mSkills = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkills>().MySkills;
        SkillUse(-this.MyEffect);
        //PlayerSkill에서 스킬을 가져와서 쿨타임 다 줄여주기
    }

    public override void Relieve()
    {
        Debug.Log("Reset Test CoolTimeDown");
        mSkills = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkills>().MySkills;
        SkillUse(this.MyEffect);
        //PlayerSkill에서 스킬을 가져와서 쿨타임 다 늘려주기
    }

    private void SkillUse(int effect)
    {
        for(int i = 0; i < mSkills.Length; i++)
        {
            if(mSkills[i] != null)
                if(!mSkills[i].isPassive)
                    mSkills[i].MyCoolTime += effect;           
        }
    }
    //패시브 스킬에 있는 Start 다 삭제
}
