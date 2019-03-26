using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;
[RequireComponent(typeof(PlayerAbility))]
[DisallowMultipleComponent]
public class PlayerAttack : PlayerAbility
{
    protected bool mbCanAttack = true;
    protected const float mAttackDelay = 0.5f;
    protected float mCheckDelay = 0.0f;
    protected BoxCollider mCheckBox;
    public float center;
    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
        mCheckBox = transform.GetChild(0).GetComponent<BoxCollider>();
        mCheckBox.center = new Vector3(0f,0f, center);
        mCheckBox.isTrigger = true;
        mCheckBox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(mCheckDelay > 0.2f)
        {
            mCheckBox.enabled = false;
        }
        if(mInputManager.AttackButton.State.CurrentState 
            == NRMInput.EButtonStates.Down
            || mInputManager.AttackButton.State.CurrentState == NRMInput.EButtonStates.Pressed
            && mbCanAttack
            )
        {
            Debug.Log("swing!");
            mCheckBox.enabled = true;
            mbCanAttack = false;
        }
        if (mInputManager.AttackButton.State.CurrentState
            == NRMInput.EButtonStates.Off
            ||mInputManager.AttackButton.State.CurrentState 
            == NRMInput.EButtonStates.Up
            && mCheckDelay > mAttackDelay)
        {
            mCheckDelay = 0f;
            mbCanAttack = true;
            mCheckBox.enabled = false;

        }
        // Debug.Log(mInputManager.AttackButton.State.CurrentState);        
        mCheckDelay += Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        EventManager.TriggerTakeDamageEvent("PlayersAttack",
            other.gameObject,mPlayer.playerStatus.Damage);
        //Debug.Log(other.tag.ToString());
    }
}
