using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillCostReduce", menuName = "Skills/Passive/SkillCostReduce", order = 1)]
public class SkillCostReduce : Skill, IUseable
{
    private Skill[] mSkills;
    public override void Use()
    {
        Debug.Log("Test CoolTimeDown");
        mSkills = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkills>().MySkills;
        SkillUse(-this.MyEffect);
    }

    public override void Relieve()
    {
        mSkills = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkills>().MySkills;
        SkillUse(this.MyEffect);
    }

    private void SkillUse(int effect)
    {
        for(int i = 0; i < mSkills.Length; i++)
        {
            if(mSkills[i] != null)
                if(!mSkills[i].isPassive)
                    mSkills[i].MyMagicPoint += effect;
        }
    }

    void Start()
    {
        this.isPassive = true;
    }
}
