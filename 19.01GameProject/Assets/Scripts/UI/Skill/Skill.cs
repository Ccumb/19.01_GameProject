using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Skill : ScriptableObject, IMoveable
{
    public bool isPassive = false;
    public int slotPosition = 0;
    [SerializeField]
    private Sprite mIcon;

    [SerializeField]
    private Sprite mExplainItem;

    [SerializeField]
    private int mCost = 0; //소모값

    [SerializeField]
    private int mMPCost= -1; //마나 다 마이너스로 설정해놓기

    [SerializeField]
    private int mCoolTime;

    [SerializeField]
    private bool mIsUseable = true;

    [SerializeField]
    private int mEffect = 0;
    private SkillSlotScript slot;
    
    public Sprite MyIcon
    {
        get
        {
            return mIcon;
        }
    }

    public Sprite MyExplainItem
    {
        get
        {
            return mExplainItem;
        }
    }

    public int MyCost
    {
        get
        {
            return mCost;
        }
        set
        {
            mCost = value;
        }
    }

    //마나
    public int MyMagicPoint
    {
        get
        {
            return mMPCost;
        }
        set
        {
            mMPCost = value;
        }
    }

    public int MyCoolTime
    {
        get
        {
            return mCoolTime;
        }
        set
        {
            mCoolTime = value;
        }
    }


    public int MyEffect
    {
        get { return mEffect; }
        set { mEffect = value; }
    }

    public bool MyIsUseable
    {
        get { return mIsUseable; }
    }
    //public void Remove()
    //{

    //}

    public SkillSlotScript MySlot
    {
        get
        {
            return slot;
        }
        set
        {
            slot = value;
        }
    }
   
    public virtual void Use() { }
    public virtual void Relieve() { }
    public virtual IEnumerator CoolTime()
    {        
        mIsUseable = false;
        int tmp = mCoolTime;
        while(MyCoolTime >= 0)
        {
            UIManager.MyInstance.UpdateCoolTimeText(this);
            mCoolTime -= 1;            
            yield return new WaitForSeconds(1.0f);
        }
        mCoolTime = tmp;
        mIsUseable = true;
    }
}
