using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectilePool))]
[DisallowMultipleComponent]
<<<<<<< HEAD
public class LongRangeAttack : MonoBehaviour
=======
public class LongRangeAttack : EnemyAbility
>>>>>>> develop
{
    public float LongTargetOffRadius;   //타겟 off 범위
    public float LongTargetOnRadius;    //타겟 on 범위

    [Range(0, 360)]
    public float LongTargetAngle;   //타겟을 인식할 각도

    public LayerMask TargetMask;    //타겟 레이어
    public LayerMask ObstacleMask;  //장애물 레이어

    bool bTargetOn = false; //타겟을 찾았는지 판별
    bool bDamage = false; //대미지를 가할 때 true
    bool bPos = false;
    float TargetOnTime = 0.5f; //타겟을 찾고 경과한 시간
    float DelayDamageTime = 3.3f; //타겟을 찾은 뒤 몇 초 뒤에 대미지를 줄 것인지
    float DamageTime = 0.0f;

<<<<<<< HEAD
=======
    ChangeSlimeColor ChangeColor;

>>>>>>> develop
    Vector3 PlayerPos = Vector3.zero;

    private SpriteRenderer RangeSpriteRenderer; //게임상에서 표시되는 2D 스프라이트(범위)

    ProjectilePool ProjectilePooling;

    //임시
    public GameObject ProejctileObejct;

    private void Awake()
    {
        this.RangeSpriteRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
<<<<<<< HEAD
        ProjectilePooling = GetComponent<ProjectilePool>();
=======
        anim = GetComponent<Animator>();
        ProjectilePooling = GetComponent<ProjectilePool>();
        ChangeColor = transform.GetChild(3).GetComponent<ChangeSlimeColor>();
>>>>>>> develop
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
        DelayDamageTime = TargetOnTime * 2;
        RangeSpriteRenderer.enabled = false;
        StartCoroutine(FindTargetsWithDelay(0.2f));
    }

    private void OnDisable()
    {
        RangeSpriteRenderer.enabled = false;
        StopCoroutine(FindTargetsWithDelay(0));
        Debug.Log("Off Script LongRange!");
    }

    void Update()
    {
        if (bTargetOn)
        {
            DamageTime += Time.deltaTime;
            if (DamageTime > TargetOnTime)
            {
                if (!bPos) bPos = true;
                if (!RangeSpriteRenderer.enabled) RangeSpriteRenderer.enabled = true;
                if (DamageTime > DelayDamageTime)
                {
                    bDamage = true;
                    bTargetOn = false;
                    DamageTime = 0.0f;
                    RangeSpriteRenderer.enabled = false;
<<<<<<< HEAD
=======
                    ChangeColor.bIsAttack = false;
>>>>>>> develop
                }
            }
        }
        else
        {
<<<<<<< HEAD
            DamageTime = 0.0f;
=======
            anim.SetBool("isAttack", false);
            RangeSpriteRenderer.enabled = false;
            DamageTime = 0.0f;
            ChangeColor.bIsAttack = false;
>>>>>>> develop
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
<<<<<<< HEAD
                    && LongTargetOffRadius < Vector3.Distance(transform.position, target.position))
                {
                    transform.forward = dirTotarget;
=======
                    && LongTargetOnRadius > Vector3.Distance(transform.position, target.position))
                {
                    if (GetComponent<EnemyMovement>().enabled == true) GetComponent<EnemyMovement>().enabled = false;
                    if(anim.GetBool("isAttack") == true) anim.SetBool("isWalk", false);
                    transform.forward = new Vector3 (dirTotarget.x, 0, dirTotarget.z);
>>>>>>> develop
                    Debug.Log("Find");
                    if (!bTargetOn) bTargetOn = true;

                    if (bPos && PlayerPos == Vector3.zero) //프로젝타일 쏠 위치
                    {
                        PlayerPos = target.position;
                    }
                    if (bDamage)
                    {
                        //프로젝타일 발사//
                        ProjectilePooling.Pooling();
<<<<<<< HEAD

                        PlayerPos = Vector3.zero;
                        bDamage = false;
                        bPos = false;
=======
                        anim.SetBool("isAttack", true);
                        PlayerPos = Vector3.zero;
                        bDamage = false;
                        bPos = false;
                        ChangeColor.bIsAttack = true;
>>>>>>> develop
                    }
                }
            }
        }

        for (int i = 0; i < TargetsInOffRadius.Length; i++)
        {
            Transform target = TargetsInOffRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < LongTargetAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
<<<<<<< HEAD
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, ObstacleMask))
                {
                    Debug.Log("Not Find");
                    //if (bTargetOn) bTargetOn = false;
=======
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, ObstacleMask)
                     && LongTargetOnRadius < Vector3.Distance(transform.position, target.position))
                {
                    Debug.Log("Not Find");
                    if (bTargetOn) bTargetOn = false;
                    if (GetComponent<EnemyMovement>().enabled == false) GetComponent<EnemyMovement>().enabled = true;
                    if (anim.GetBool("isAttack") == false) anim.SetBool("isWalk", true);
>>>>>>> develop
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
}
