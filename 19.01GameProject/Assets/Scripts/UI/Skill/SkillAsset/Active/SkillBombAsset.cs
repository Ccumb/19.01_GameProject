using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActiveSkillBomb", menuName = "Skills/Active/ActiveSkillBomb", order = 1)]
public class SkillBombAsset : Skill, IUseable
{
    public GameObject BombPrefab;

    // Update is called once per frame
    public override void Use()
    {
        GameObject bomb = Instantiate(BombPrefab, GameObject.FindGameObjectWithTag("Player").transform);
        ActiveSkillBomb activeSkill = bomb.GetComponent<ActiveSkillBomb>();
        activeSkill.Damage = this.MyEffect;
    }
}
