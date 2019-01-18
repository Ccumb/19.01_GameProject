using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EnemyAttack_Box : MonoBehaviour
{
    private BoxCollider mAttackBound;

    public float attackDelay;       // 공격 주기
    public float maintainBound;     // 공격 영역 유지 시간

    public float attackPower;

    private bool isPlaying;     // 기지모 관련 변수(신경쓰지 않으셔도 됨)

    // Start is called before the first frame update
    void Start()
    {
        mAttackBound = GetComponent<BoxCollider>();
        mAttackBound.isTrigger = true;
        mAttackBound.enabled = false;

        if(attackPower == 0)
        {
            attackPower = gameObject.GetComponentInParent<Enemy>().power;
        }

        isPlaying = true;
    }

    private void OnEnable()
    {
        StartCoroutine("ActiveBound");
    }

    // Update is called once per frame
    void Update()
    {
        bool isArrive = gameObject.GetComponentInParent<Enemy>().isArrive;

        if(!isArrive)
        {
            StopCoroutine("ActiveBound");
        }
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
            Player player = other.gameObject.GetComponent<Player>();
            player.TakeDamage(attackPower);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 center = this.transform.position;

        Vector3 size = mAttackBound.size;

        if (!isPlaying)
        {
            Gizmos.DrawWireCube(center, size);
        }
        else if (mAttackBound.enabled)  //이 부분때문에 null ref 오류나는데, 신경 굳이 안쓰셔도 됩니다!
        {
            Gizmos.DrawWireCube(center, size);
        }
    }
}
