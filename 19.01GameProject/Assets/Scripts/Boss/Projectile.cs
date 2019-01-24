using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    Rigidbody ProejctileRigid;
    private void Awake()
    {
        ProejctileRigid = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        ProejctileRigid.isKinematic = false;
        ProejctileRigid.useGravity = false;
    }

    private void OnEnable()
    {
        //ProejctileRigid.velocity = new Vector3(transform.parent.forward.x, 0, transform.parent.forward.z) * 10;
    }

    private void OnDisable()
    {
        ProejctileRigid.velocity = Vector3.zero;
    }
}
