using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemyAttack_Sphere : MonoBehaviour
{
    private SphereCollider mAttackBound;

    public float attackDelay;       // 공격 주기
    public float maintainBound;     // 공격 영역 유지 시간

    public float attackPower;

    // Start is called before the first frame update
    void Start()
    {
        mAttackBound = GetComponent<SphereCollider>();
        mAttackBound.isTrigger = true;
        mAttackBound.enabled = false;

        if (attackPower == 0)
        {
            attackPower = gameObject.GetComponentInParent<Enemy>().power;
        }
    }

    private void OnEnable()
    {
        StartCoroutine("ActiveBound");
    }

    // Update is called once per frame
    void Update()
    {
        bool isArrive = gameObject.GetComponentInParent<Enemy>().isArrive;

        if (!isArrive)
        {
            StopCoroutine("ActiveBound");
        }
    }

    IEnumerator ActiveBound()
    {
        while(true)
        {
            yield return new WaitForSeconds(attackDelay);

            mAttackBound.enabled = true;

            yield return new WaitForSeconds(maintainBound);

            mAttackBound.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Hit Player(Sphere)");
            Player player = other.gameObject.GetComponent<Player>();
            player.TakeDamage(attackPower);
        }
    }
}
