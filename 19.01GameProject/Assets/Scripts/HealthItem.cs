﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class HealthItem : Item
{
    public float amount;

    private void Start()
    {
        mIsActive = true;    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.hp += amount;

            if(player.hp > player.max_hp)
            {
                player.hp = player.max_hp;
            }

            StartCoroutine("Respawn");

            InActive();
        }
    }
}