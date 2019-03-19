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
        // 플레이어 클래스 겹쳐서 일단 playera로 수정, 확인바람
        Playera player = GameObject.FindGameObjectWithTag("Player").GetComponent<Playera>();
        if(player.hp < player.max_hp)
        {
            Remove();

            player.hp += mHealthRec;
        }        
    }
}
