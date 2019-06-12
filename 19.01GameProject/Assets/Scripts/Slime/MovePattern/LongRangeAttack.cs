using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectilePool))]
[DisallowMultipleComponent]
public class LongRangeAttack : EnemyAbility
{
    public float LongTargetOffRadius;   //타겟 off 범위
    public float LongTargetOnRadius;    //타겟 on 범위
    [Range(0, 360)]
    public float LongTargetAngle;   //타겟을 인식할 각도

    public float TargetOnTime = 0.5f; //타겟을 찾고 경과한 시간
    public float DamageTime = 3.3f; //타겟을 찾은 뒤 몇 초 뒤에 대미지를 줄 것인지
    public float CoolTime = 1.0f;

    public LayerMask TargetMask;    //타겟 레이어
    public LayerMask ObstacleMask;  //장애물 레이어

    private bool mbTargetOn = false; //타겟을 찾았는지 판별
    private bool mbDamage = false; //대미지를 가할 때 true
    private bool mbCool = false;
    private float mCoolTime = 0.0f;
    private float mDamageTime = 0.0f;

    private SpriteRenderer mRangeSpriteRenderer; //게임상에서 표시되는 2D 스프라이트(범위)

    private ProjectilePool mProjectilePooling;

    private void Awake()
    {
        mRangeSpriteRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    protected override void Initialization()
    {
        base.Initialization();

        //초기화 할 것들
        SetAnimBool("isAttack", false);
        SetAnimBool("isWalk", false);
        SetAnimBool("isJump", false);
        SetAnimBool("isDie", false);
        mProjectilePooling = GetComponent<ProjectilePool>();
        Debug.Log("LongRangeAttackInit");
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
        Debug.Log("On Script LongRange!");
        DamageTime = TargetOnTime * 2;
        mRangeSpriteRenderer.enabled = false;
        StartCoroutine(FindTargetsWithDelay(0.2f));
    }

    private void OnDisable()
    {
        if (_enemyMovement.enabled) _enemyMovement.enabled = false;
        SetAnimBool("isAttack", false);
        SetAnimBool("isWalk", false);
        SetAnimBool("isJump", false);
        mRangeSpriteRenderer.enabled = false;
        StopCoroutine(FindTargetsWithDelay(0));
        Debug.Log("Off Script LongRange!");
    }

    void Update()
    {
        if (mbTargetOn && !mbCool)
        {
            mDamageTime += Time.deltaTime;
            if (mDamageTime > TargetOnTime)
            {
                if (!mRangeSpriteRenderer.enabled) mRangeSpriteRenderer.enabled = true;
                if ((mDamageTime > DamageTime) )
                {
                    mbDamage = true;
                    mDamageTime = 0.0f;
                    mRangeSpriteRenderer.enabled = false;
                    ChangeColor.bIsAttack = true;
                    mbCool = true;
                }
            }
        }
        else
        {
            SetAnimBool("isAttack", false);
            mRangeSpriteRenderer.enabled = false;
            mDamageTime = 0.0f;
        }


        if (mbCool)
        {
            mCoolTime += Time.deltaTime;
            if (mCoolTime > CoolTime)
            {
                mbCool = false;
                mCoolTime = 0.0f;
            }
        }
    }

    void FindVisibleTargets()
    {
        Collider[] TargetsInOnRadius = Physics.OverlapSphere(transform.position, LongTargetOnRadius, TargetMask);
        Collider[] TargetsInOffRadius = Physics.OverlapSphere(transform.position, LongTargetOffRadius, TargetMask);

        for (int i = 0; i < TargetsInOnRadius.Length; i++) //Target Off로 만듦
        {
            Transform target = TargetsInOnRadius[i].transform;
            Vector3 dirTotarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirTotarget) < LongTargetAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirTotarget, dstToTarget, ObstacleMask)
                    && LongTargetOnRadius > Vector3.Distance(transform.position, target.position))
                {
                    if (!mbTargetOn) mbTargetOn = true;
                    if (_enemyMovement.enabled) _enemyMovement.enabled = false;
                    if(GetAnimBool("isAttack") || mbTargetOn) SetAnimBool("isWalk", false);
                    transform.forward = new Vector3 (dirTotarget.x, 0, dirTotarget.z);

                    if (mbDamage)
                    {
                        //프로젝타일 발사//
                        mProjectilePooling.Pooling();
                        SetAnimBool("isAttack", true); 
                        mbDamage = false;
                        ChangeColor.bIsAttack = false;
                    }
                }
            }
        }
        //Target off
        for (int i = 0; i < TargetsInOffRadius.Length; i++)
        {
            Transform target = TargetsInOffRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < LongTargetAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, ObstacleMask)
                     && LongTargetOnRadius < Vector3.Distance(transform.position, target.position))
                {
                    if (mbTargetOn) mbTargetOn = false;
                    if (!_enemyMovement.enabled) _enemyMovement.enabled = true;
                    if (!GetAnimBool("isAttack") && !mbTargetOn) SetAnimBool("isWalk", true);
                }
            }
        }
    }
}
