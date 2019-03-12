using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float mPrimaryAvoidDistance;

    private float mIncreasedSpeed;
    private int mIncreasedDamage;
    private int mIncreasedMaxHP;
    private int mIncreasedMaxSP;
    private int mIncreasedMaxSkillCost;
    private float mIncreasedAvoidDistance;

    private float mSpeed;
    private int mDamage;
    private int mMaxHP;
    private int mHP;
    private int mMaxSP;
    private int mSP;
    private int mMaxSkillCost;
    private int mSkillCost;
    private float mAvoidDistance;

    public float Speed { get { return mSpeed; } }
    public float Damage { get { return mDamage; } }
    public float MaxHP { get { return mMaxHP; } }
    public float HP { get { return mHP; } }
    public float MaxSP { get { return mMaxSP; } }
    public float SP { get { return mSP; } }
    public float MaxSkillCost { get { return mMaxSkillCost; } }
    public float SkillCost { get { return mSkillCost; } }
    public float AvoidDistance { get { return mAvoidDistance; } }

    private void Awake()
    {
        InitializeStatus();
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
        mPrimaryAvoidDistance = 5f;

        mSpeed = mPrimarySpeed;
        mDamage = mPrimaryDamage;
        mMaxHP = mPrimaryMaxHP;
        mHP = mPrimaryHP;
        mMaxSP = mPrimaryMaxSP;
        mSP = mPrimarySP;
        mMaxSkillCost = mPrimaryMaxSkillCost;
        mSkillCost = mPrimarySkillCost;
        mAvoidDistance = mPrimaryAvoidDistance;

    }
    private void UpdateStatus()
    {
        mSpeed = mPrimarySpeed + mIncreasedSpeed;
        mDamage = mPrimaryDamage + mIncreasedDamage;
        mMaxHP = mPrimaryMaxHP + mIncreasedMaxHP;
        mMaxSP = mPrimaryMaxSP + mIncreasedMaxSP;
        mMaxSkillCost = mPrimaryMaxSkillCost + mIncreasedMaxSkillCost;
        mAvoidDistance = mPrimaryAvoidDistance + mIncreasedAvoidDistance;
    }
}
