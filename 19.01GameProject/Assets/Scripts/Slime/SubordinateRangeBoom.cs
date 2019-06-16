using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;

public class SubordinateRangeBoom : MonoBehaviour
{
    public int Damage = 1; //대미지
    public float BoomDelayTime = 5.0f; //지연 시간
    public float BoomRadius; // 대미지를 적용할 범위
    [Range(0, 360)]
    public float TargetAngle;   //타겟을 인식할 각도

    public LayerMask TargetMask;    //타겟 레이어
    public LayerMask ObstacleMask;  //장애물 레이어

    public ParticleSystem Explosion = null;
    public float ParticleScale = 10.0f;

    public AudioClip BoomAudio;
    private AudioSource mBoomSource;

    private float mBoomTime = 0.0f;
    private bool mbBoom = false;

    private SpriteRenderer mRangeSpriteRenderer; //게임상에서 표시되는 2D 스프라이트(범위)

    private void OnEnable()
    {
        mRangeSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        mRangeSpriteRenderer.transform.localScale = new Vector3(BoomRadius, BoomRadius, 0) * 10.0f;
        mRangeSpriteRenderer.enabled = false;

        mBoomSource = GameObject.Find("Sound").GetComponent<AudioSource>();
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

    private void Update()
    {
        if(!mbBoom)
        {
            mBoomTime += Time.deltaTime;
            if (mBoomTime > BoomDelayTime - 1)
            {
                if(!mRangeSpriteRenderer.enabled)
                    mRangeSpriteRenderer.enabled = true;
            }
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

        for (int i = 0; i < targetsInOnRadius.Length; i++)
        {
            Transform target = targetsInOnRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < TargetAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, ObstacleMask))
                {
                    //대미지 적용 함수
                    if(mbBoom)
                    {
                        DamageArea(targetsInOnRadius, Damage);
                        Explosion.transform.localScale = new Vector3(ParticleScale, ParticleScale, ParticleScale);
                        Instantiate(Explosion, transform.position, Quaternion.identity);
                        DamageArea(targetsInOnRadius, Damage);
                        if (BoomAudio != null)
                        {
                            Debug.Log("Audio");
                            mBoomSource.PlayOneShot(BoomAudio);
                        }
                        mRangeSpriteRenderer.enabled = false;
                        gameObject.SetActive(false);
                        return;
                    }
                }
            }
        }
        //범위 내에 플레이어가 없어도 일정시간이 지나면 터지도록 하기 위하여 추가
        if (mbBoom)
        {
            Explosion.transform.localScale = new Vector3(ParticleScale, ParticleScale, ParticleScale);
            Instantiate(Explosion, transform.position, Quaternion.identity);
            DamageArea(targetsInOnRadius, Damage);
            if (BoomAudio != null)
            {
                Debug.Log("Audio");
                mBoomSource.PlayOneShot(BoomAudio);
            }
            mRangeSpriteRenderer.enabled = false;
            gameObject.SetActive(false);
        }
    }

    void DamageArea(Collider[] plyaerObjects, int damage)
    {
        foreach (Collider player in plyaerObjects)
        {
            if (player.GetComponent<Player>() != null)
            {
                Debug.Log("Damage[RangeAttackScript]: " + damage);
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
