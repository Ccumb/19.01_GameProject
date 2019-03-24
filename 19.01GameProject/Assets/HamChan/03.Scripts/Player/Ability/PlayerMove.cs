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
        mHorizontalMove = mInputManager.PrimaryMovement.x;
        mVerticalMove = mInputManager.PrimaryMovement.y;
        mDirectionVector = Vector3.forward * mVerticalMove + Vector3.right * mHorizontalMove;
        if (mInputManager.IsMoving())
        {

            transform.Translate(Vector3.forward * mPlayerStatus.Speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(mDirectionVector);

        }
    }

}
