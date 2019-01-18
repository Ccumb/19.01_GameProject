using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : EnemyAbility
{
    public float speed = 0.5f;

    private GameObject mTarget;

    private void Start()
    {
        Enemy owner = GetComponent<Enemy>();
        owner.RegisterAbility(this);

        mTarget = GameObject.FindGameObjectWithTag("Player");
        Rigidbody rigid = GetComponent<Rigidbody>();
        rigid.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, mTarget.transform.position, step);
    }
}
