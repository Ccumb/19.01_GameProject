using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;

public class DamageArea : MonoBehaviour
{
    [HideInInspector]
    public bool bDamaged = false; //True일 경우에만 대미지 가함
    public float DamageDelayTime = 1.0f; //대미지를 주는 주기
    public float Damage = 1.0f; //대미지

    private float mDamageTime = 0.0f;
    private GameObject mDamageObject = null; //맵 오브젝트
    

    private void OnEnable()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    private void OnDisable()
    {
        bDamaged = false;
    }

    private void Update()
    {
        if(bDamaged)
        {
            mDamageTime += Time.deltaTime;
            if(mDamageTime > DamageDelayTime)
            {
                if (mDamageObject != null)
                {
                    Debug.Log("Damage[DamageAreaScript]: " + Damage);
                    //플레이어의 컴포넌트를 가지고 와서 대미지 주는 함수
                    EventManager.TriggerTakeDamageEvent("EnemysAttack", mDamageObject, (int)Damage);
                }
                mDamageTime = 0.0f;
            }
        }
        else
        {
            mDamageTime = 0.0f;
        }
    }

    /// <summary>
    /// 안으로 들어 왔을 때 대미지가 들어가도록 설정
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("In");
            mDamageObject = collision.gameObject;
            bDamaged = true;
        }
    }

    /// <summary>
    /// 밖으로 나가면 대미지를 입지 않음
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision)
    {
        if (bDamaged && collision.gameObject.tag == "Player")
        {
            Debug.Log("Out");
            mDamageObject = null;
            bDamaged = false;
        }
    }
}
