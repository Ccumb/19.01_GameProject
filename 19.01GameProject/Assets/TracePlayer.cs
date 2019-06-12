using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracePlayer : MonoBehaviour
{
    public float speed = 0.5f;

    private GameObject mTarget;

    void Start()
    {
        Enemy owner = GetComponent<Enemy>();

        mTarget = GameObject.FindGameObjectWithTag("Player");
        Rigidbody rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, mTarget.transform.position, step);
    }
}
