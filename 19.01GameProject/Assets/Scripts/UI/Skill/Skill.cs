using UnityEngine;

public class Skill : ScriptableObject, IMoveable
{
    public int slotPosition = 0;
    [SerializeField]
    private Sprite mIcon;

    [SerializeField]
    private Sprite mExplainItem;

    [SerializeField]
    private int mUsingCost;

    [SerializeField]
    private int mDamage;

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

    public int MyUsingCost
    {
        get
        {
            return mUsingCost;
        }
    }
    
    public int MyDamage
    {
        get
        {
            return mDamage;
        }
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
}
