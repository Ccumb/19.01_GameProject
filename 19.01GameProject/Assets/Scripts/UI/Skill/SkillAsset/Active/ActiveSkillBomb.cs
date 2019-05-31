using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;

public class ActiveSkillBomb : MonoBehaviour
{
    public int Damage = 0;
    public int DestroyWaitTime = 3;

    public GameObject effect;
    private GameObject mEffectInstance;

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
        mEffectInstance = Instantiate(effect, this.transform);

        Collider[] colliders = Physics.OverlapSphere(this.transform.position,5.0f);
        foreach(Collider hit in colliders)
        {
            if(hit.tag == "Monster")
            {
                EventManager.TriggerTakeDamageEvent("PlayersAttack", hit.gameObject, Damage);
                //데미지 주는거 여따가 쓰기
            }
        }
        DisableObject();
    }

    private void DisableObject()
    {
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<CapsuleCollider>().enabled = false;

        StartCoroutine("Destroy");
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(mEffectInstance);
        Destroy(this.gameObject);
    }
}
