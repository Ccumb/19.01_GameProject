using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private Rigidbody mProejctileRigid;
    public int ProejctileDamage = 1;
    public float DelayActive = 1.0f;

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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Damage: " + ProejctileDamage);
        if (collision.gameObject.tag == "Player")
        {
            EventManager.TriggerTakeDamageEvent("EnemysAttack", collision.gameObject, (int)ProejctileDamage);
            gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    IEnumerator RemoveProjectile(float delay)
    {
        Debug.Log("RmoveStart");
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false); Debug.Log("Rmove");
    }
}
