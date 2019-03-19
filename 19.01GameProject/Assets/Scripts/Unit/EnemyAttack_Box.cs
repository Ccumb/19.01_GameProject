using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EnemyAttack_Box : EnemyAbility
{
    private BoxCollider mAttackBound;

    public float attackDelay;       // 공격 주기
    public float maintainBound;     // 공격 영역 유지 시간

    public float attackPower;
    

    // Start is called before the first frame update
    void Start()
    {
        Enemy owner = GetComponentInParent<Enemy>();
        owner.RegisterAbility(this);

        mAttackBound = GetComponent<BoxCollider>();
        mAttackBound.isTrigger = true;
        mAttackBound.enabled = false;

        if(attackPower == 0)
        {
            attackPower = gameObject.GetComponentInParent<Enemy>().power;
        }
    }

    private void OnEnable()
    {
        StartCoroutine("ActiveBound");
    }

    private void OnDisable()
    {
        mAttackBound.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator ActiveBound()
    {
        while (true)
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
            Debug.Log("Hit Player(Box)");
            Playera player = other.gameObject.GetComponent<Playera>();
            player.TakeDamage(attackPower);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 center = this.transform.position;

        //이 부분때문에 null ref 오류나는데, 신경 굳이 안쓰셔도 됩니다!

        Vector3 size = mAttackBound.size;
        
        if (mAttackBound.enabled)  
        {
            Gizmos.DrawWireCube(center, size);
        }
    }
}
