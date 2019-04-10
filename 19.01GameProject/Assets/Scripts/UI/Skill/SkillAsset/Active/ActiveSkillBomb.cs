using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkillBomb : MonoBehaviour
{
    public int Damage = 0;
    public int DestroyWaitTime = 3;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(DestroyWaitTime);
        Bomb();
    }

    private void Bomb()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position,5.0f);
        foreach(Collider hit in colliders)
        {
            if(hit.tag == "Monster")
            {
                //데미지 주는거 여따가 쓰기
            }
        }
        Destroy(this.gameObject);
    }
}
