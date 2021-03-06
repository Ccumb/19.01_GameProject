﻿using UnityEngine;

public abstract class ItemIconVersion : ScriptableObject
{
    public int slotPosition = 0;
    [SerializeField]
    private Sprite mIcon;

    [SerializeField]
    private Sprite mExplainItem;

    [SerializeField]
    private int mStackSize;

    private SlotScript slot;



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

    // 아이템이 중첩될 수 있는 개수
    // 예) 소모성 물약의 경우 한개의 Slot에 여러개가
    //     중첩되어서 보관될 수 있음.
    public int MyStackSize
    {
        get
        {
            return mStackSize;
        }
    }

    public void Remove()
    {
        if(MySlot != null)
        {
            MySlot.RemoveItem(this);
        }
    }

    public SlotScript MySlot
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
