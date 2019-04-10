using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillLongDistanceAttackAsset", menuName = "Skills/Active/SkillLongDistanceAttackAsset", order = 1)]
public class ActiveSkillLongDistanceAttackAsset : Skill, IUseable
{
    public GameObject BulletPrefab;

    // Update is called once per frame
    public override void Use()
    {
        ActiveSkillBomb bomb = Instantiate(BulletPrefab, GameObject.FindGameObjectWithTag("Player").transform).GetComponent<ActiveSkillBomb>();
        bomb.Damage = this.MyEffect;

    }
}
