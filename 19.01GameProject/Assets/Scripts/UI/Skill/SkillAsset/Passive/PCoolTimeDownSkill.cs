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
                {
                    mSkills[i].mOriginalCoolTime += effect;
                }
            //오리지날 쿨타임하고 바뀌는 쿨타임이 같지 않으면 해주지 않기!
        }
    }
    //패시브 스킬에 있는 Start 다 삭제
}
