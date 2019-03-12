using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.AI;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerAbility))]
public class PlayerMove : PlayerAbility
{
    private Rigidbody mRigidbody;
    protected void Awake()
    {
    }
    protected void Start()
    {
        base.Start();
        mRigidbody = GetComponent<Rigidbody>();
        mRigidbody.isKinematic = true;
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        moveHorizontal = moveHorizontal * Time.deltaTime;
        moveVertical = moveVertical * Time.deltaTime;
        transform.Translate(Vector3.right * moveHorizontal * mPlayerStatus.Speed);
        transform.Translate(Vector3.forward * moveVertical * mPlayerStatus.Speed);
    }

}
