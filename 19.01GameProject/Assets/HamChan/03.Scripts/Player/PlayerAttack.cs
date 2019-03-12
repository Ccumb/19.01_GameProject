using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(PlayerAbility))]
public class PlayerAttack : PlayerAbility
{
    private bool mbCanAttack = true;
    private const float mAttackDelay = 0.5f;
    private float mCheckDelay = 0.0f;
    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(mInputManager.AttackButton.State.CurrentState 
            == NRMInput.EButtonStates.Down
            || mInputManager.AttackButton.State.CurrentState == NRMInput.EButtonStates.Pressed
            && mbCanAttack
            )
        {
            Debug.Log("swing!");
            mbCanAttack = false;
        }
        if(mInputManager.AttackButton.State.CurrentState == NRMInput.EButtonStates.Off
            && mCheckDelay > mAttackDelay)
        {
            mCheckDelay = 0f;
            mbCanAttack = true;
        }
        Debug.Log(mInputManager.AttackButton.State.CurrentState);        
        mCheckDelay += Time.deltaTime;
    }
}
