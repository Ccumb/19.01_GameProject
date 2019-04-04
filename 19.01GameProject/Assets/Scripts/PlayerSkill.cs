using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;

public class PlayerSkill : MonoBehaviour
{
     
    private const int mMaxSkillAmount = 4;

    private  Skill[] mSkills = new Skill[mMaxSkillAmount];

    public void RegistSkill(List<ActionButton> actions)
    {
        Debug.Log("Test Regist Skill");
        for(int i = 0; i < actions.Count; i++)
        {
            mSkills[i] = actions[i].MyUseable as Skill;
        }
        CheckPassiveSkill();
    }

    public void CheckPassiveSkill()
    {
        Debug.Log(mSkills.Length);
       /* for(int i = 0; i < mSkills.Length; i++)
        {
            if(mSkills[i].isPassive)
                mSkills[i].Use();
        }*/
    }

    private void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            mSkills[i] = null;
        }

        UIManager.MyInstance.Regist += new UpdatePlayerSkillList(RegistSkill);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(mSkills[0] != null)
            {
                if(mSkills[0].MyIsUseable && !(mSkills[0].isPassive))
                {
                    mSkills[0].Use();
                    StartCoroutine(mSkills[0].CoolTime());                    
                    //패시브 스킬일때 코루틴을 으케 처리할까잉
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            if(mSkills[1] != null)
                mSkills[1].Use();
        }
    }

    
    /*
    private const int mMaxSkillAmount = 4;
    
    [SerializeField] 
    private Skill[] mSkills = new Skill[mMaxSkillAmount];
    
    private void Start()
    {
        for(int i = 0; i < 4; i ++)
        {
            mSkills[i] = new Skill();
        }
    }
    private void Update()
    {
        //키가 눌리고 캐스트할 마나가 있는지 체크
        if(mInputManager.Skill1Button.State.CurrentState == NRMInput.EButtonStates.Down
            || mInputManager.Skill1Button.State.CurrentState == NRMInput.EButtonStates.Pressed
            && mPlayerStatus.SP >= mSkills[0].MyUsingCost)
        {
            EventManager.TriggerIntEvent("CastSkill", mSkills[0].MyUsingCost);
            mSkills[0].Use();
        }
        if(mInputManager.Skill2Button.State.CurrentState == NRMInput.EButtonStates.Down
            || mInputManager.Skill2Button.State.CurrentState == NRMInput.EButtonStates.Pressed
            && mPlayerStatus.SP >= mSkills[1].MyUsingCost)
        {
            EventManager.TriggerIntEvent("CastSkill", mSkills[1].MyUsingCost);
            mSkills[1].Use();
        }
        if(mInputManager.Skill3Button.State.CurrentState == NRMInput.EButtonStates.Down
            || mInputManager.Skill3Button.State.CurrentState == NRMInput.EButtonStates.Pressed
            && mPlayerStatus.SP >= mSkills[2].MyUsingCost)
        {
            EventManager.TriggerIntEvent("CastSkill", mSkills[2].MyUsingCost);
            mSkills[2].Use();
        }
        if(mInputManager.Skill4Button.State.CurrentState == NRMInput.EButtonStates.Down
            || mInputManager.Skill4Button.State.CurrentState == NRMInput.EButtonStates.Pressed
            && mPlayerStatus.SP >= mSkills[3].MyUsingCost)
        {
            EventManager.TriggerIntEvent("CastSkill", mSkills[3].MyUsingCost);
            mSkills[3].Use();
        }
    }

    protected virtual void RegistSkill(Skill[] skills)
    {
        for(int i = 0; i < mMaxSkillAmount; i++)
        {
            if(skills[i] != null)
            {
                mSkills[i] = skills[i];                
            }
        }
    }
*/
    //protected virtual void RemoveSkill(Skill skill)
    //{
    //    for(int i = 0; i < mMaxSkillAmount; i++)
    //    {
    //        if(mSkills[i] == skill)
    //        {
    //            mSkills[i] = new EmptySkill();
    //        }
    //    }
    //}
}
