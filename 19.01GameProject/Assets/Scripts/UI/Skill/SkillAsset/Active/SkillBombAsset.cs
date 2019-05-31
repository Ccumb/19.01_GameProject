﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActiveSkillBomb", menuName = "Skills/Active/ActiveSkillBomb", order = 1)]
public class SkillBombAsset : Skill, IUseable
{
    public GameObject BombPrefab;

    // Update is called once per frame
    public override void Use()
    {
        GameObject bomb = Instantiate(BombPrefab);
        bomb.transform.position 
            = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x,
                            GameObject.FindGameObjectWithTag("Player").transform.position.y + 0.7f,
                            GameObject.FindGameObjectWithTag("Player").transform.position.z);
        ActiveSkillBomb activeSkill = bomb.GetComponent<ActiveSkillBomb>();
        activeSkill.Damage = this.MyEffect;
    }
}
