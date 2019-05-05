﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/Potion/HealthPotion")]
public class HealthPotion : ItemIconVersion, IUseable
{
    public int MyCost
    {
        get; set;
    }

    //회복량
    [SerializeField]
    private int mHealthRec;

    public void Use()
    {
        // 플레이어 클래스 겹쳐서 일단 playera로 수정, 확인바람
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(player.playerStatus.HP < player.playerStatus.MaxHP)
        {
            Remove();
            EventManager.TriggerIntEvent("EatHealthPosition", mHealthRec);
        }        
    }
}
