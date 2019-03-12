using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class HealthItem : Item
{
    public float amount;

    private void Start()
    {
        mbIsActive = true;    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Playera player = collision.gameObject.GetComponent<Playera>();
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
