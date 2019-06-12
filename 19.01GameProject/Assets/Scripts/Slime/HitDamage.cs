using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;

public class HitDamage : MonoBehaviour
{
    [HideInInspector]
    public bool bDamaged = false; //True일 경우에만 대미지 가함
    public float DamageDelayTime = 3.0f; //대미지를 주는 주기
    public int Damage = 10; //대미지

    private float mDamageTime = 0.0f;
    private GameObject mDamageObject = null; //맵 오브젝트
    private void OnEnable()
    {
        bDamaged = true;
    }
    
    void Update()
    {
        mDamageTime += Time.deltaTime;
        if (mDamageTime > DamageDelayTime)
        {
            if(!bDamaged)
               bDamaged = true;
        }

    }

    private void OnDisable()
    {
        bDamaged = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
        if (collision.gameObject.tag == "Player" && bDamaged)
        {
            Debug.Log("Damage[HitDamageScript]: " + Damage);
            //플레이어의 컴포넌트를 가지고 와서 대미지 주는 함수
            EventManager.TriggerTakeDamageEvent("EnemysAttack", mDamageObject, Damage);
            mDamageTime = 0.0f;
            bDamaged = false;
        }
    }
}
