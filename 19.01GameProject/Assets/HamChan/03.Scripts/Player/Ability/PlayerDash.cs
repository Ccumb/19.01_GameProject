using System.Collections;
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
    private void Start()
    {
        base.Start();
        mRigidbody = GetComponent<Rigidbody>();
        mPlayerCollider = GetComponent<Collider>();
        mRigidbody.isKinematic = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if(mInputManager.DashButton.State.CurrentState 
            == NRMInput.EButtonStates.Down
            || mInputManager.DashButton.State.CurrentState 
            == NRMInput.EButtonStates.Pressed)
        {
            mPlayerCollider.enabled = false;
            Debug.Log("Hit");
            transform.Translate(Vector3.forward * mPlayerStatus.DashDistance *5*Time.deltaTime);
        }
    }
    private void OnCollider()
    {
        //애니메이션 종료 후 켜기;
        mPlayerCollider.enabled = true;
    }
}
