using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAbility))]
[DisallowMultipleComponent]
public class SlimeManager : MonoBehaviour
{
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
        if(Input.GetKeyDown(KeyCode.W))
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
}
