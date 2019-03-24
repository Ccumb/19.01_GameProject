using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;
[DisallowMultipleComponent]
public class PlayerStatus :MonoBehaviour
{
    private float mPrimarySpeed;
    private int mPrimaryDamage;
    private int mPrimaryMaxHP;
    private int mPrimaryHP;
    private int mPrimaryMaxSP;
    private int mPrimarySP;
    private int mPrimaryMaxSkillCost;
    private int mPrimarySkillCost;
    private float mPrimaryDashDistance;

    private float mIncreasedSpeed;
    private int mIncreasedDamage;
    private int mIncreasedMaxHP;
    private int mIncreasedMaxSP;
    private int mIncreasedMaxSkillCost;
    private float mIncreasedDashDistance;

    private float mSpeed;
    private int mDamage;
    private int mMaxHP;
    private int mHP;
    private int mMaxSP;
    private int mSP;
    private int mMaxSkillCost;
    private int mSkillCost;
    private float mDashDistance;

    public float Speed { get { return mSpeed; } }
    public int Damage { get { return mDamage; } }
    public int MaxHP { get { return mMaxHP; } }
    public int HP { get { return mHP; } }
    public int MaxSP { get { return mMaxSP; } }
    public int SP { get { return mSP; } }
    public int MaxSkillCost { get { return mMaxSkillCost; } }
    public int SkillCost { get { return mSkillCost; } }
    public float DashDistance { get { return mDashDistance; } }

    private void Awake()
    {
        InitializeStatus();
    }
    private void OnEnable()
    {
        EventManager.StartListeningIntEvent("CastSkill", SetSP);
    }
    private void OnDisable()
    {
        EventManager.StopListeningIntEvent("CastSkill", SetSP);
    }
    private void InitializeStatus()
    {
        mPrimarySpeed = 6f;
        mPrimaryDamage = 1;
        mPrimaryMaxHP = 100;
        mPrimaryHP = 100;
        mPrimaryMaxSkillCost = 6;
        mPrimarySkillCost = 6;
        mPrimaryMaxSP = 100;
        mPrimarySP = 100;
        mPrimaryDashDistance = 5f;

        mSpeed = mPrimarySpeed;
        mDamage = mPrimaryDamage;
        mMaxHP = mPrimaryMaxHP;
        mHP = mPrimaryHP;
        mMaxSP = mPrimaryMaxSP;
        mSP = mPrimarySP;
        mMaxSkillCost = mPrimaryMaxSkillCost;
        mSkillCost = mPrimarySkillCost;
        mDashDistance = mPrimaryDashDistance;

    }
    private void UpdateStatus()
    {
        mSpeed = mPrimarySpeed + mIncreasedSpeed;
        mDamage = mPrimaryDamage + mIncreasedDamage;
        mMaxHP = mPrimaryMaxHP + mIncreasedMaxHP;
        mMaxSP = mPrimaryMaxSP + mIncreasedMaxSP;
        mMaxSkillCost = mPrimaryMaxSkillCost + mIncreasedMaxSkillCost;
        mDashDistance = mPrimaryDashDistance + mIncreasedDashDistance;
    }
    private void SetSP(int i)
    {
        if(mSP +i > mMaxSP)
        {
            mSP = mMaxSP;
        }
        else if(mSP + i < 0)
        {
            mSP = 0;
        }
        else
        {
            mSP += i;
        }
    }
}
