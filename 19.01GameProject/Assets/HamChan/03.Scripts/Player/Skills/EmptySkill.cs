using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptySkill : Skill
{
    private void Awake()
    {
        base.Awake();
        Initialization("Empty", -1, -1);
    }
    public override void Active()
    {
        Debug.Log("This slot is empty!");
    }
}
