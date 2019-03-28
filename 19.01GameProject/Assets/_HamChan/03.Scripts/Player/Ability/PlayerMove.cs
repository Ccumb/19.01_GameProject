using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.AI;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerAbility))]
[DisallowMultipleComponent]
public class PlayerMove : PlayerAbility
{
    protected Rigidbody mRigidbody;
    protected Vector3 mDirectionVector;
    protected float mHorizontalMove;
    protected float mVerticalMove;
    
    protected void Start()
    {
        base.Start();
        mRigidbody = GetComponent<Rigidbody>();
        mRigidbody.isKinematic = true;
    }

    private void Update()
    {
        mHorizontalMove = mPlayer.linkedInputManager.PrimaryMovement.x;
        mVerticalMove = mPlayer.linkedInputManager.PrimaryMovement.y;
        mDirectionVector = Vector3.forward * mVerticalMove + Vector3.right * mHorizontalMove;
        if (mPlayer.linkedInputManager.IsMoving())
        {
            transform.Translate(Vector3.forward * mPlayer.playerStatus.Speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(mDirectionVector);
            mPlayer.animator.SetBool("Walk", true);
        }
        else
        {
            mPlayer.animator.SetBool("Walk", false);
        }
    }

}
