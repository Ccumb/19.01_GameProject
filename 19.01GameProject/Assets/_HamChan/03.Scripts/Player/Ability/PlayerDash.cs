﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerAbility))]
[DisallowMultipleComponent]
public class PlayerDash : PlayerAbility
{
    private Rigidbody mRigidbody;
    private Collider mPlayerCollider;
    public float speed;
    private void Start()
    {
        base.Start();
        mRigidbody = GetComponent<Rigidbody>();
        mPlayerCollider = GetComponent<Collider>();
        mRigidbody.isKinematic = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if(mInputManager.DashButton.State.CurrentState 
            == NRMInput.EButtonStates.Down
            //|| mInputManager.DashButton.State.CurrentState 
            //== NRMInput.EButtonStates.Pressed)
            )
        {
            mPlayer.animator.SetTrigger("Dash");
            mPlayerCollider.enabled = false;
            Debug.Log("Hit");
            transform.Translate(Vector3.forward * mPlayerStatus.DashDistance *5*Time.deltaTime);
        }
    }
    private void DashEnd()
    {
        //애니메이션 종료 후 켜기;
        mPlayerCollider.enabled = true;
    }
}
