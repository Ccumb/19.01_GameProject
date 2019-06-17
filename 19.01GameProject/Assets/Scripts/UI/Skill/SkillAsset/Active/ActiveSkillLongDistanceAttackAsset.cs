using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillLongDistanceAttackAsset", menuName = "Skills/Active/SkillLongDistanceAttackAsset", order = 1)]
public class ActiveSkillLongDistanceAttackAsset : Skill, IUseable
{
    public GameObject BulletPrefab;
    private AudioSource mSource;
    // Update is called once per frame
    public override void Use()
    {
        if(this.MyIsUseable)
        {
            mSource = GameObject.Find("Canvas").GetComponent<AudioSource>();
            mSource.clip = this.MyClip;
            mSource.volume = 0.2f;
            mSource.Play();
            GameObject bullet = Instantiate(BulletPrefab);
            LongDistanceAttack activeSkill = bullet.GetComponent<LongDistanceAttack>();
            activeSkill.Damage = this.MyEffect;
        }
    }
}
