using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;
public class PlayerSkills : PlayerAbility
{
    private const int mMaxSkillAmount = 4;
    protected Skill_Ham[] mSkills = new Skill_Ham[mMaxSkillAmount];
    private void Start()
    {
        base.Start();
        for (int i = 0; i < 4; i++)
        {
            mSkills[i] = EmptySkill.Instance;
        }
        
    }
    private void Update()
    {
        //키가 눌리고 캐스트할 마나가 있는지 체크
        if(mInputManager.Skill1Button.State.CurrentState == NRMInput.EButtonStates.Down
            || mInputManager.Skill1Button.State.CurrentState == NRMInput.EButtonStates.Pressed
            && mPlayerStatus.SP >= mSkills[0].CastCost)
        {
            EventManager.TriggerIntEvent("CastSkill", mSkills[0].CastCost);
            mSkills[0].Active();
        }
        if (mInputManager.Skill2Button.State.CurrentState == NRMInput.EButtonStates.Down
            || mInputManager.Skill2Button.State.CurrentState == NRMInput.EButtonStates.Pressed 
            && mPlayerStatus.SP >= mSkills[1].CastCost)
        {
            EventManager.TriggerIntEvent("CastSkill", mSkills[1].CastCost);
            mSkills[1].Active();
        }
        if (mInputManager.Skill3Button.State.CurrentState == NRMInput.EButtonStates.Down
            || mInputManager.Skill3Button.State.CurrentState == NRMInput.EButtonStates.Pressed
            && mPlayerStatus.SP >= mSkills[2].CastCost)
        {
            EventManager.TriggerIntEvent("CastSkill", mSkills[2].CastCost);
            mSkills[2].Active();
        }
        if (mInputManager.Skill4Button.State.CurrentState == NRMInput.EButtonStates.Down
            || mInputManager.Skill4Button.State.CurrentState == NRMInput.EButtonStates.Pressed
            && mPlayerStatus.SP >= mSkills[3].CastCost)
        {
            EventManager.TriggerIntEvent("CastSkill", mSkills[3].CastCost);
            mSkills[3].Active();
        }
    }
    protected virtual void RegistSkill(Skill_Ham skill)
    {
        for(int i = 0; i<mMaxSkillAmount; i++)
        {
            if(mSkills[i].Name == "Empty")
            {
                mSkills[i] = skill;
                return;
            }
        }
        mSkills[mMaxSkillAmount - 1] = skill;
    }
    protected virtual void RemoveSkill(Skill skill)
    {
        for (int i = 0; i < mMaxSkillAmount; i++)
        {
            if(mSkills[i] == skill)
            {
                mSkills[i] = new EmptySkill();
            }

        }
    }
}
