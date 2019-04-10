using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;
[DisallowMultipleComponent]
public class PlayerSkills : PlayerAbility
{
    private const int mMaxSkillAmount = 4;

    private Skill[] mSkills = new Skill[mMaxSkillAmount];

    public Skill[] MySkills
    {
        get { return mSkills; }
    }

    public void RegistSkill(List<ActionButton> actions)
    {
        lock(mSkills)
        {
            OffPassiveSkill();
            for(int i = 0; i < actions.Count; i++)
            {
                mSkills[i] = actions[i].MyUseable as Skill;
            }
            OnPassiveSkill();
        }
    }

    public void OnPassiveSkill()
    {
        for(int i = 0; i < mSkills.Length; i++)
        {
            if(mSkills[i] != null)
                if(mSkills[i].isPassive)
                    mSkills[i].Use();
        }
    }

    public void OffPassiveSkill()
    {
        for(int i = 0; i < mSkills.Length; i++)
        {
            if(mSkills[i] != null)
                if(mSkills[i].isPassive)
                    mSkills[i].Relieve();
        }
    }

    private void UsingSkill(Skill skill)
    {
        if(skill == null)
            return;
        if(skill.MyIsUseable && !(skill.isPassive))
        {
            EventManager.TriggerIntEvent("CastSkill", skill.MyMagicPoint);
            skill.Use();
            StartCoroutine(skill.CoolTime());
            Debug.Log(mPlayer.playerStatus.SP);
        }
    }

    private void Start()
    {
        base.Start();

        for(int i = 0; i < 4; i++)
        {
            mSkills[i] = null;
        }

        UIManager.MyInstance.Regist += new UpdatePlayerSkillList(RegistSkill);
    }


    private void Update()
    {
        //키가 눌리고 캐스트할 마나가 있는지 체크
        if(mInputManager.Skill1Button.State.CurrentState == NRMInput.EButtonStates.Down
            || mInputManager.Skill1Button.State.CurrentState == NRMInput.EButtonStates.Pressed
            && mPlayerStatus.SP >= mSkills[0].MyMagicPoint)
        {
            UsingSkill(mSkills[0]);
            //EventManager.TriggerIntEvent("CastSkill", mSkills[0].MyMagicPoint);
            //mSkills[0].Use();
            
        }
        if (mInputManager.Skill2Button.State.CurrentState == NRMInput.EButtonStates.Down
            || mInputManager.Skill2Button.State.CurrentState == NRMInput.EButtonStates.Pressed 
            && mPlayerStatus.SP >= mSkills[1].MyMagicPoint)
        {
            UsingSkill(mSkills[1]);
            //EventManager.TriggerIntEvent("CastSkill", mSkills[1].MyMagicPoint);
            //mSkills[1].Use();
        }
        if (mInputManager.Skill3Button.State.CurrentState == NRMInput.EButtonStates.Down
            || mInputManager.Skill3Button.State.CurrentState == NRMInput.EButtonStates.Pressed
            && mPlayerStatus.SP >= mSkills[2].MyMagicPoint)
        {
            UsingSkill(mSkills[2]);
            //EventManager.TriggerIntEvent("CastSkill", mSkills[2].MyMagicPoint);
            //mSkills[2].Use();
        }
        if (mInputManager.Skill4Button.State.CurrentState == NRMInput.EButtonStates.Down
            || mInputManager.Skill4Button.State.CurrentState == NRMInput.EButtonStates.Pressed
            && mPlayerStatus.SP >= mSkills[3].MyMagicPoint)
        {
            UsingSkill(mSkills[3]);
            //EventManager.TriggerIntEvent("CastSkill", mSkills[3].MyMagicPoint);
            //mSkills[3].Use();
        }
    }


    //protected virtual void RemoveSkill(Skill skill)
    //{
    //    for (int i = 0; i < mMaxSkillAmount; i++)
    //    {
    //        if(mSkills[i] == skill)
    //        {
    //            mSkills[i] = new EmptySkill();
    //        }

    //    }
    //}
}
