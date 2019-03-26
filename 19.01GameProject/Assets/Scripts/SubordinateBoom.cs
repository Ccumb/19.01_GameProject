using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;

public class SubordinateBoom : MonoBehaviour
{
    public int Damage = 1;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //대미지 주는 함수
            EventManager.TriggerTakeDamageEvent("EnemysAttack", collision.gameObject, Damage);
            Debug.Log("Damage!");
            gameObject.SetActive(false);
        }
    }
}
