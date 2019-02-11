using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/Potion", order = 2)]
public class HealthPotion : ItemIconVersion, IUseable
{
    //회복량
    [SerializeField]
    private int mHealthRec;

    public void Use()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(player.hp < player.max_hp)
        {
            Remove();

            player.hp += mHealthRec;
        }
    }
}
