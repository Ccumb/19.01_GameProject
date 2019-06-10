using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;

public class SubordinateBoom : MonoBehaviour
{
    public int Damage = 1;

    /// <summary>
    /// 플레이어와 부딪혔을 때 대미지를 주고 폭탄 터짐
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //대미지 주는 함수
            EventManager.TriggerTakeDamageEvent("EnemysAttack", collision.gameObject, Damage);
            Debug.Log("Damage[SubordinateBoom]: " + Damage);
            //이펙트 차후 추가
            gameObject.SetActive(false);
            
        }
    }
}
