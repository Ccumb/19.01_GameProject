using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;
public class Skill_Ham : Singleton<Skill_Ham>
{
    protected string mName;
    protected int mCost;
    protected int mCastCost;

    public string Name { get { return mName; } }
    public int Cost { get { return mCost; } }
    public int CastCost { get { return mCastCost; } }
    protected void Awake()
    {
        base.Awake();
    }
    public virtual void Active()
    {
        Debug.Log("This slot is empty!");
    }
    protected virtual void Initialization(string name, int cost, int castCost)
    {
        mName = name;
        mCost = cost;
        mCastCost = castCost;
    }
}
