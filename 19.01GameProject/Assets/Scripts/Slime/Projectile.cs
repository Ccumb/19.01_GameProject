using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    Rigidbody ProejctileRigid;
    public int ProejctileDamage = 1;

    private void Awake()
    {
        ProejctileRigid = GetComponent<Rigidbody>();
        gameObject.SetActive(false);
    }
    private void Start()
    {
        ProejctileRigid.isKinematic = false;
        ProejctileRigid.useGravity = false;
    }

    private void OnEnable()
    {
        ProejctileRigid.velocity = new Vector3(transform.parent.GetChild(0).forward.x, 0, transform.parent.GetChild(0).forward.z) * 10;
    }

    private void OnDisable()
    {
        ProejctileRigid.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(ProejctileDamage);
            gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
