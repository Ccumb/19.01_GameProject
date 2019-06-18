using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyAbility : MonoBehaviour
{
    [HideInInspector]
    public Animator anim = null;
    protected ChangeSlimeColor ChangeColor = null;
    protected EnemyMovement _enemyMovement = null;

    protected AudioSource MonsterAudio;

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
        MonsterAudio = GetComponent<AudioSource>();
        Debug.Log("EnemyAbility Init");
    }

    /// <summary>
    /// 슬라임 애니메이터 불 변수 겟
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool GetAnimBool(string name)
    {
        return anim.GetBool(name);
    }
    /// <summary>
    /// 슬라임 애니메이터 불 변수 셋
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public void SetAnimBool(string name, bool value)
    {
        anim.SetBool(name, value);
    }

    /// <summary>
    /// 에디터 앵글 조절
    /// </summary>
    /// <param name="angleInDegrees"></param>
    /// <param name="angleIsGlobal"></param>
    /// <returns></returns>
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
