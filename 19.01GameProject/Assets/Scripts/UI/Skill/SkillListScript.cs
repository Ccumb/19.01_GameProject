using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillListScript : MonoBehaviour
{
    [SerializeField]
    private GameObject mSlotPrefab;

    private List<SkillSlotScript> mSlots = new List<SkillSlotScript>();

    private List<bool> own = new List<bool>();

    public List<SkillSlotScript> MySlots
    {
        get
        {
            return mSlots;
        }
    }

    public List<bool> MyOwn
    {
        get
        {
            return own;
        }
    }

    public List<bool> GetOwnSkillList()
    {
        for(int i = 0; i < MySlots.Count; i ++)
        {
            if(!MySlots[i].IsEmpty)
                own[i] = true;
            else
                own[i] = false;
        }
        return own;
    }

    public void AddSlots(int slotCount)
    {
        for (int i = 0; i < slotCount; i++)
        {
            SkillSlotScript slot = Instantiate(mSlotPrefab, transform).GetComponent<SkillSlotScript>();
            mSlots.Add(slot);
            own.Add(false);
        }
    }

    public bool AddSkill(Skill skill)
    {
        Debug.Log(skill.name);
        //mSlots[skill.slotPosition].AddSkill(skill);
        if (skill.name == "HealingSkill(Clone)")
        {
            mSlots[skill.slotPosition].AddSkill(skill);
            return true;
        }
        if(skill.name == "CoolTimeDownSkill(Clone)")
        {
            mSlots[skill.slotPosition].AddSkill(skill);
            return true;
        }
        if(skill.name == "ActiveSkillBomb(Clone)")
        {
            mSlots[skill.slotPosition].AddSkill(skill);
            return true;
        }
        if(skill.name == "PassiveHeal(Clone)")
        {
            mSlots[skill.slotPosition].AddSkill(skill);
            return true;
        }
        if(skill.name == "BasicAttackPowerIncrease(Clone)")
        {
            mSlots[skill.slotPosition].AddSkill(skill);
            return true;
        }
        if(skill.name == "SkillShortDistanceAttackAsset(Clone)")
        {
            mSlots[skill.slotPosition].AddSkill(skill);
            return true;
        }
        if(skill.name == "SkillLongDistanceAttackAsset(Clone)")
        {
            mSlots[skill.slotPosition].AddSkill(skill);
            return true;
        }
        

        return false;
    }
}
