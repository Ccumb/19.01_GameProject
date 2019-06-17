using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;

[CreateAssetMenu(fileName = "HealingSkill", menuName = "Skills/Active/Heal", order = 1)]
public class HealingSkill : Skill, IUseable
{
    private AudioSource mSource;
    public override void Use()
    {
        if(this.MyIsUseable)
        {
            mSource = GameObject.Find("Canvas").GetComponent<AudioSource>();
            mSource.clip = this.MyClip;
            mSource.volume = 0.2f;
            mSource.Play();
            PlayerStatus player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
            if(player.HP >= player.MaxHP)
            {
                this.MyCoolTime = 0;
                EventManager.TriggerIntEvent("CastSkill", -this.MyMagicPoint);
                return;
            }
            else if(player.HP < player.MaxHP)
            {
                //플레이어 체력회복
                //player.set += this.MyEffect;
                EventManager.TriggerIntEvent("EatHealthPosition", MyEffect);
            }                                 
        }        
    }
}
