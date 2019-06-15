using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private Rigidbody mProejctileRigid;
    public int ProejctileDamage = 1;
    public float DelayActive = 1.0f; //프로젝타일이 사라지는 시간

    public ParticleSystem Explosion = null;
    public float ParticleScale = 1.0f;

    private void Awake()
    {
        mProejctileRigid = GetComponent<Rigidbody>();
        gameObject.SetActive(false);
    }
    private void Start()
    {
        mProejctileRigid.isKinematic = false;
        mProejctileRigid.useGravity = false;
    }

    private void OnEnable()
    {
        StartCoroutine(RemoveProjectile(DelayActive));
        mProejctileRigid.velocity = new Vector3(transform.parent.GetChild(0).forward.x, 0, transform.parent.GetChild(0).forward.z) * 10;
    }

    private void OnDisable()
    {
        StopCoroutine(RemoveProjectile(0));
        mProejctileRigid.velocity = Vector3.zero;
    }

    /// <summary>
    /// 플레이어에게 맞으면 대미지 후 액티브 false, 다른 물체에 부딪히면 액티브 false
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit Projectile"+collision.gameObject.name);
        Explosion.transform.localScale = new Vector3(ParticleScale, ParticleScale, ParticleScale);
        Debug.Log("Damage: " + ProejctileDamage);
        if (collision.gameObject.tag == "Player")
        {
            EventManager.TriggerTakeDamageEvent("EnemysAttack", collision.gameObject, ProejctileDamage);
            Instantiate(Explosion, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
        Instantiate(Explosion, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 다른 물체에 맞지 않더라도 일정 시간이 지나면 프로젝타일 액티브 false
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    IEnumerator RemoveProjectile(float delay)
    {
        Debug.Log("RmoveStart");
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false); Debug.Log("Rmove");
    }
}
