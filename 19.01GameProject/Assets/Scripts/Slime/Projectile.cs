﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    Rigidbody ProejctileRigid;
    public int ProejctileDamage = 1;

    private void Awake()
    {
        ProejctileRigid = GetComponent<Rigidbody>();
        gameObject.SetActive(false);
    }
    private void Start()
    {
        ProejctileRigid.isKinematic = false;
        ProejctileRigid.useGravity = false;
    }

    private void OnEnable()
    {
        ProejctileRigid.velocity = new Vector3(transform.parent.GetChild(0).forward.x, 0, transform.parent.GetChild(0).forward.z) * 10;
    }

    private void OnDisable()
    {
        ProejctileRigid.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Damage: " + ProejctileDamage);
        if (collision.gameObject.tag == "Player")
        {
            EventManager.TriggerTakeDamageEvent("EnemysAttack", collision.gameObject, (int)ProejctileDamage);
            gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
