using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAbility))]
[DisallowMultipleComponent]
public class SlimeManager : Enemy
{
    enum EPhase
    {
        Non
            , One
            , Two
            , Three
            , Four
            , Die
    }

    public int[] PhaseOne;
    public int[] PhaseTwo;
    public int[] PhaseThree;
    public int[] PhaseFour;

    private EPhase mPresentPhase = EPhase.Non;
    private EPhase mPastPhase = EPhase.Non;

    [HideInInspector]
    public List<EnemyAbility> Ability = null;

    private Component[] AbilityComponent;

    void Start()
    {
        AbilityComponent = GetComponentsInParent(typeof(EnemyAbility));

        if (AbilityComponent != null)
        {
            foreach (EnemyAbility ability in AbilityComponent)
                Ability.Add(ability);
        }

        if (GetComponent<EnemyAbility>() != null) Ability.Remove(GetComponent<EnemyAbility>());
        if (GetComponent<EnemyMovement>() != null) Ability.Remove(GetComponent<EnemyMovement>());
    }

    void Update()
    {
        if (hp > 80)
        {
            mPresentPhase = EPhase.One;
        }
        else if(hp <= 80 && hp > 40)
        {
            mPresentPhase = EPhase.Two;
        }
        else if (hp <= 40 && hp > 10)
        {
            mPresentPhase = EPhase.Three;
        }
        else if (hp <= 10 && hp > 0)
        {
            mPresentPhase = EPhase.Four;
        }
        else
        {
            mPresentPhase = EPhase.Die;
        }

        if(mPresentPhase != mPastPhase)
        {
            Debug.Log("Change");
            PatternManager(mPresentPhase);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if(Ability[0].enabled == true) Ability[0].enabled = false;
            else Ability[0].enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (Ability[1].enabled == true) Ability[1].enabled = false;
            else Ability[1].enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (Ability[2].enabled == true) Ability[2].enabled = false;
            else Ability[2].enabled = true;
        }
    }

    void PatternManager(EPhase phaseNum)
    {
        mPastPhase = phaseNum;
        switch (phaseNum)
        {
            case EPhase.One:
                SetAbilityFalse();
                for (int i =0; i< PhaseOne.Length; i++ )
                    Ability[PhaseOne[i]].enabled = true;
                break;
            case EPhase.Two:
                SetAbilityFalse();
                for (int i = 0; i < PhaseTwo.Length; i++)
                    Ability[PhaseTwo[i]].enabled = true;
                break;
            case EPhase.Three:
                SetAbilityFalse();
                for (int i = 0; i < PhaseThree.Length; i++)
                    Ability[PhaseThree[i]].enabled = true;
                break;
            case EPhase.Four:
                SetAbilityFalse();
                for (int i = 0; i < PhaseFour.Length; i++)
                    Ability[PhaseFour[i]].enabled = true;
                break;
            case EPhase.Die:
                Die();
                break;
            default:
                break;
        }

    }

    void SetAbilityFalse()
    {
        for (int i = 0; i < Ability.Count; i++)
        {
            Ability[i].enabled = false;
        }
    }
}
