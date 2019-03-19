using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyAbility : MonoBehaviour
{
    protected Animator anim = null;
    protected ChangeSlimeColor ChangeColor = null;
    protected EnemyMovement _enemyMovement = null;
    protected virtual void Start()
    {
        Initialization();
    }

    protected virtual void Initialization()
    {
        //꼭 필요한 것만 초기화
        anim = GetComponent<Animator>();
        ChangeColor = transform.GetChild(3).GetComponent<ChangeSlimeColor>();
        _enemyMovement = GetComponent<EnemyMovement>();
        Debug.Log("EnemyAbility Init");
    }

    //Slime animator bool get;
    public bool GetAnimBool(string name)
    {
        return anim.GetBool(name);
    }
    //Slime animator bool set;
    public void SetAnimBool(string name, bool value)
    {
        anim.SetBool(name, value);
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
