using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;

[CreateAssetMenu(fileName = "HealingSkill", menuName = "Skills/Active/Heal", order = 1)]
public class HealingSkill : Skill, IUseable
{
    public override void Use()
    {
        if(this.MyIsUseable)
        {
            PlayerStatus player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
            EventManager.TriggerCommonEvent("ActiveHeal");
            if (player.HP >= player.MaxHP)
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
