using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// 체력과 공격력을 가진 유닛이 상속받는 베이스 클래스
///</summary>
public class Unit : MonoBehaviour
{
    public float hp;    // 체력
    public float max_hp;        // 최대 체력

    public float power; // 공격력

    // 죽을때 처리 함수
    protected virtual void Die()
    {
        Destroy(this.gameObject);
    }

    protected void InitHP()
    {
        if (hp > max_hp)
        {
            max_hp = hp;
        }
    }

    // 데미지를 입는 함수
    public void TakeDamage(float damage)
    {
        hp -= damage;
    }
}
