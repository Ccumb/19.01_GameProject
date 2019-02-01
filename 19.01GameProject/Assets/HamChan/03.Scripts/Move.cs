﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.AI;
public class Move : MonoBehaviour
{
    public float speed;

    private Rigidbody mRigidbody;

    void Start()
    {
        mRigidbody = GetComponent<Rigidbody>();
        mRigidbody.isKinematic = true;
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        moveHorizontal = moveHorizontal * Time.deltaTime;
        moveVertical = moveVertical * Time.deltaTime;
        transform.Translate(Vector3.right * moveHorizontal * 6);
        transform.Translate(Vector3.forward * moveVertical * 6);
    }

}
