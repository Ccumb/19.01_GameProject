using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    protected string mName = "Empty";
    protected int mCost = -1;
    protected int mCastCost = -1;

    public string Name { get { return mName; } }
    public int Cost { get { return mCost; } }
    public int CastCost { get { return mCastCost; } }
    public virtual void Active()
    {
    }
    protected virtual void Initialization(string name, int cost, int castCost)
    {
        mName = name;
        mCost = cost;
        mCastCost = castCost;
    }
}
