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
    private int mUsingCost = -1; //마나

    [SerializeField]
    private int mCoolTime;

    [SerializeField]
    private bool mIsUseable = true;
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

    public int MyUsingCost
    {
        get
        {
            return mUsingCost;
        }
        set
        {
            mUsingCost = value;
        }
    }

    public int MyCoolTime
    {
        get
        {
            return mCoolTime;
        }

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
        int tmp = this.MyCoolTime;
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
