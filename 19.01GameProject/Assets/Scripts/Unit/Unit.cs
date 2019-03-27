using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;
///<summary>
/// 체력과 공격력을 가진 유닛이 상속받는 베이스 클래스
///</summary>
public class Unit : MonoBehaviour
{
    public float hp;    // 체력
    public float max_hp;        // 최대 체력

    public float power;

    public float respawnTime;

    public float activeFalseTime;

    protected bool mbIsActive;

    public bool IsArrive
    {
        get
        {
            return mbIsActive;
        }
    }
    private void OnEnable()
    {
        EventManager.StartListeningTakeDamageEvent("PlayersAttack", TakeDamage);
    }
    private void OnDisable()
    {
        EventManager.StopListeningTakeDamageEvent("PlayersAttack", TakeDamage);
    }

    // 죽을때 처리 함수
    protected virtual void Die()
    {
        //Destroy(this.gameObject);
    }

    protected void InitHP()
    {
        if (hp > max_hp)
        {
            max_hp = hp;
        }
    }
    protected virtual void TakeDamage(GameObject gameObejct,int damage)
    {
        if(gameObejct == this.gameObject)
        {
            hp -= damage;
        }

    }
    // 데미지를 입는 함수
    public void TakeDamage(float damage)
    {
        hp -= damage;
    }

    protected virtual void Active()
    {
        if (mbIsActive == false)
        {
            MeshRenderer meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
            meshRenderer.enabled = true;
            Collider collider = this.gameObject.GetComponent<Collider>();
            collider.enabled = true;

            mbIsActive = true;
        }
    }

    protected virtual void InActive()
    {
        if (mbIsActive == true)
        {
            MeshRenderer meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
            meshRenderer.enabled = false;
            Collider collider = this.gameObject.GetComponent<Collider>();
            collider.enabled = false;

            mbIsActive = false;
        }
    }
}
