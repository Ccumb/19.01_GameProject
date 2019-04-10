using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillListScript : MonoBehaviour
{
    [SerializeField]
    private GameObject mSlotPrefab;

    private List<SkillSlotScript> mSlots = new List<SkillSlotScript>();

    public List<SkillSlotScript> MySlots
    {
        get
        {
            return mSlots;
        }
    }

    public void AddSlots(int slotCount)
    {
        for (int i = 0; i < slotCount; i++)
        {
            SkillSlotScript slot = Instantiate(mSlotPrefab, transform).GetComponent<SkillSlotScript>();
            mSlots.Add(slot);
        }
    }

    public bool AddSkill(Skill skill)
    {
        Debug.Log(skill.name);
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

        return false;
    }
}
