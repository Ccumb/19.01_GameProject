using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;

[DisallowMultipleComponent]
public class RangeAttack : EnemyAbility
{
    public float TargetOffRadius;   //타겟 off 범위
    public float TargetOnRadius;    //타겟 on 범위
    [Range(0, 360)]
    public float TargetAngle;   //타겟을 인식할 각도

    public float TargetOnTime = 0.5f; //타겟을 찾고 경과한 시간
    public float DamageTime = 0.5f; //타겟을 찾은 뒤 몇 초 뒤에 대미지를 줄 것인지
    public float CoolTime = 1.0f;
    public float RangeDamage = 1.0f;
    public float RepulsiveForce = 10.0f;

    public bool bRepulsion = false;

    public LayerMask TargetMask;    //타겟 레이어
    public LayerMask ObstacleMask;  //장애물 레이어

    private bool mbTargetOn = false; //타겟을 찾았는지 판별
    private bool mbDamage = false; //대미지를 가할 때 true
    private bool mbCool = false;
    private float mCoolTime = 0.0f;
    private float mDamageTime = 0.0f;

    private SpriteRenderer mRangeSpriteRenderer; //게임상에서 표시되는 2D 스프라이트(범위)

    private void Awake()
    {
        mRangeSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    protected override void Initialization()
    {
        base.Initialization();

        //초기화 할 것들
        SetAnimBool("isAttack", false);
        SetAnimBool("isWalk", false);
        mRangeSpriteRenderer.transform.localScale = new Vector3(TargetOnRadius, TargetOnRadius, 0) * 4.5f;
        DamageTime = DamageTime + TargetOnTime;
        Debug.Log("RangeAttackInit");
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargetsInternal();
        }
    }

    /// <summary>
    /// 스크립트가 켜졌을 때 스프라이트 렌더를 끄고 코루틴 실행 (스프라이트 렌더는 공격 시에만 켜지기 때문에 확인차 꺼줌)
    /// </summary>
    private void OnEnable()
    {
        Debug.Log("On Script Range!");
        mRangeSpriteRenderer.enabled = false;
        StartCoroutine(FindTargetsWithDelay(0.2f));
    }

    /// <summary>
    /// 스크립트가 꺼졌을 때 스프라이트 렌더를 끄고 코루틴 종료
    /// </summary>
    private void OnDisable()
    {
        mRangeSpriteRenderer.enabled = false;
        StopCoroutine(FindTargetsWithDelay(0));
        Debug.Log("Off Script Range!");
    }

    /// <summary>
    /// 시간에 따른 공격, 애니메이션 false 등
    /// </summary>
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
            mDamageTime = 0.0f;
            mRangeSpriteRenderer.enabled = false;
            ChangeColor.bIsAttack = false;
        }

        if(mbCool)
        {
            mCoolTime += Time.deltaTime;
            if(mCoolTime > CoolTime)
            {
                mbCool = false;
                mCoolTime = 0.0f;
            }
        }
    }

    /// <summary>
    /// 타겟을 찾아내는 함수 애니메이션 변경, 공격 실행 등을 여기에서 처리
    /// </summary>
    private void FindVisibleTargetsInternal()
    {
        Collider[] targetsInOnRadius = Physics.OverlapSphere(transform.position, TargetOnRadius, TargetMask);
        Collider[] targetsInOffRadius = Physics.OverlapSphere(transform.position, TargetOffRadius, TargetMask);

        for (int i = 0; i < targetsInOnRadius.Length; i++)
        {
            Transform target = targetsInOnRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < TargetAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, ObstacleMask))
                {
                    if (!mbTargetOn) mbTargetOn = true;
                    if (_enemyMovement.enabled) _enemyMovement.enabled = false;
                    if (GetAnimBool("isAttack") || mbTargetOn) SetAnimBool("isWalk", false);
                    transform.forward = new Vector3(dirToTarget.x, 0, dirToTarget.z);

                    if (mbDamage)
                    {
                        //범위 대미지를 주는 함수//
                        DamageArea(targetsInOnRadius, RangeDamage);
                        SetAnimBool("isAttack", true);
                        mbDamage = false;
                        ChangeColor.bIsAttack = false;
                        return;
                    }
                }
            }
        }        

        for (int i = 0; i < targetsInOffRadius.Length; i++) //Target Off로 만듦
        {
            Transform target = targetsInOffRadius[i].transform;
            Vector3 dirTotarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirTotarget) < TargetAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirTotarget, dstToTarget, ObstacleMask)
                    && TargetOnRadius < Vector3.Distance(transform.position, target.position))
                {
                    if (mbTargetOn) mbTargetOn = false;
                    if (!_enemyMovement.enabled) _enemyMovement.enabled = true;
                    if (!GetAnimBool("isAttack") && !mbTargetOn) SetAnimBool("isWalk", true);
                }
            }
        }        
    }

    /// <summary>
    /// 범위 안으로 들어온 객체 중 플레이어에게만 대미지를 주는 함수
    /// </summary>
    /// <param name="plyaerObjects"></param>
    /// <param name="damage"></param>
    void DamageArea(Collider[] plyaerObjects, float damage)
    {
        foreach (Collider player in plyaerObjects)
        {
            if (player.GetComponent<Player>() != null)
            {
                Debug.Log("Damage[RangeAttackScript]: " + damage);
                EventManager.TriggerTakeDamageEvent("EnemysAttack", player.gameObject, (int)damage);

                //True, False를 이용해서 끄고 키기로 사용/비사용
                if (bRepulsion)
                {
                    player.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * RepulsiveForce, ForceMode.Acceleration);
                }
            }
        }
    }
}
