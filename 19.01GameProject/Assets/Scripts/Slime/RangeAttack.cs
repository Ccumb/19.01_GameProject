using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class RangeAttack : EnemyAbility
{
    public float TargetOffRadius;   //타겟 off 범위
    public float TargetOnRadius;    //타겟 on 범위
    [Range(0, 360)]
    public float TargetAngle;   //타겟을 인식할 각도

    public LayerMask TargetMask;    //타겟 레이어
    public LayerMask ObstacleMask;  //장애물 레이어

    bool bTargetOn = false; //타겟을 찾았는지 판별
    bool bDamage = false; //대미지를 가할 때 true
    float TargetOnTime = 0.5f; //타겟을 찾고 경과한 시간
    float DelayDamageTime = 0.5f; //타겟을 찾은 뒤 몇 초 뒤에 대미지를 줄 것인지
    float DamageTime = 0.0f;

    [HideInInspector]
    public SpriteRenderer RangeSpriteRenderer; //게임상에서 표시되는 2D 스프라이트(범위)

    private void Awake()
    {
        RangeSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        RangeSpriteRenderer.transform.localScale = new Vector3(TargetOnRadius, TargetOnRadius, 0) * 10;
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    private void OnEnable()
    {
        Debug.Log("On Script Range!");
        DelayDamageTime = TargetOnTime * 2;
        RangeSpriteRenderer.enabled = false;
        StartCoroutine(FindTargetsWithDelay(0.2f));
    }

    private void OnDisable()
    {
        RangeSpriteRenderer.enabled = false;
        StopCoroutine(FindTargetsWithDelay(0));
        Debug.Log("Off Script Range!");
    }

    void Update()
    {
        if (bTargetOn)
        {
            DamageTime += Time.deltaTime;
            if (DamageTime > TargetOnTime)
            {
                if (!RangeSpriteRenderer.enabled) RangeSpriteRenderer.enabled = true;
                if (DamageTime > DelayDamageTime)
                {
                    bDamage = true;
                    bTargetOn = false;
                    DamageTime = 0.0f;
                    RangeSpriteRenderer.enabled = false;
                }
            }
        }
        else
        {
            DamageTime = 0.0f;
        }
    }

    void FindVisibleTargets()
    {
        Collider[] TargetsInOnRadius = Physics.OverlapSphere(transform.position, TargetOnRadius, TargetMask);
        Collider[] TargetsInOffRadius = Physics.OverlapSphere(transform.position, TargetOffRadius, TargetMask);

        for (int i = 0; i < TargetsInOffRadius.Length; i++) //Target Off로 만듦
        {
            Transform target = TargetsInOffRadius[i].transform;
            Vector3 dirTotarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirTotarget) < TargetAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirTotarget, dstToTarget, ObstacleMask)
                    && TargetOnRadius < Vector3.Distance(transform.position, target.position))
                {
                    if (bTargetOn) bTargetOn = false;
                    Debug.Log("Target Off");
                }
            }
        }

        for (int i = 0; i < TargetsInOnRadius.Length; i++)
        {
            Transform target = TargetsInOnRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < TargetAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, ObstacleMask))
                {
                    if (!bTargetOn) bTargetOn = true;

                    if (bDamage)
                    {
                        //범위 대미지를 주는 함수//
                        AreaDamage(TargetsInOnRadius, 1.0f);
                        bDamage = false;
                    }
                }
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    void AreaDamage(Collider[] objects, float damage)
    {
        foreach (Collider player in objects)
        {
            if (player.GetComponent<Player>() != null)
            {
                player.GetComponent<Player>().TakeDamage(damage);
            }
        }
    }
}
