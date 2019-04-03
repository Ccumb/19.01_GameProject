using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "HealingSkill", menuName = "Skills/Heal", order = 1)]
public class HealingSkill : Skill, IUseable
{
    public int recovery = 10;

    public override void Use()
    {
        if(this.MyIsUseable)
        {
            Playera player = GameObject.FindGameObjectWithTag("Player").GetComponent<Playera>();
            if(player.hp < player.max_hp)
            {
                player.hp += recovery;
            }                                 
        }        
    }
}
