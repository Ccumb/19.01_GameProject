using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;

public class DamageArea : MonoBehaviour
{
    [HideInInspector]
    public bool bDamaged = false;
    public float DamageDelayTime = 1.0f;
    public float Damage = 1.0f;

    private float mDamageTime = 0.0f;
    private GameObject mDamageObject = null;
    

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
                Debug.Log("Damage");
                if (mDamageObject != null)
                {
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("In");
            mDamageObject = collision.gameObject;
            bDamaged = true;
        }
    }

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
