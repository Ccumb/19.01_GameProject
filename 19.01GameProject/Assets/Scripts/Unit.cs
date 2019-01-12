﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float hp;
    public float power;


    protected void Die()
    {
        Destroy(this.gameObject);
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
    }
}
