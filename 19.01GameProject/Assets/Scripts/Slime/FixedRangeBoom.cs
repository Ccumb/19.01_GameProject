using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;

public class FixedRangeBoom : MonoBehaviour
{
    public int Damage = 1; //대미지
    public float BoomDelayTime = 5.0f; //지연 시간
    public float BoomRadius; // 대미지를 적용할 범위
    [Range(0, 360)]
    public float TargetAngle;   //타겟을 인식할 각도

    public LayerMask TargetMask;    //타겟 레이어

    public float mBoomTime = 0.0f;
    private bool mbBoom = false;

    private void OnEnable()
    {
        mbBoom = false;
        StartCoroutine(FindTargetsWithDelay(0.2f));
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
    /// 터지지 않을 때만 시간이 감
    /// </summary>
    private void Update()
    {
        if (!mbBoom)
        {
            mBoomTime += Time.deltaTime;
            if (mBoomTime > BoomDelayTime)
            {
                mbBoom = true;
                mBoomTime = 0.0f;
            }
        }
    }


    private void FindVisibleTargetsInternal()
    {
        Collider[] targetsInOnRadius = Physics.OverlapSphere(transform.position, BoomRadius, TargetMask);
                
        if (mbBoom)
        {
            //이펙트 차후 추가
            DamageArea(targetsInOnRadius, Damage);
            mbBoom = false;
            Debug.Log("The boom!");
            return;
        }
    }

    /// <summary>
    /// 대미지 적용 함수, Collider 배열 전체에게 대미지를 가함
    /// </summary>
    /// <param name="plyaerObjects"></param>
    /// <param name="damage"></param>
    void DamageArea(Collider[] plyaerObjects, int damage)
    {
        foreach (Collider player in plyaerObjects)
        {
            if (player.GetComponent<Player>() != null)
            {
                Debug.Log("Damage[FixedRangeBoom]: " + damage);
                EventManager.TriggerTakeDamageEvent("EnemysAttack", player.gameObject, damage);
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
