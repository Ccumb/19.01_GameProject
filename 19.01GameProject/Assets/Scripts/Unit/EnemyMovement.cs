using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : EnemyAbility
{
    public float speed = 0.5f;
    public AudioClip MoveAudio;
    private GameObject mTarget;

    protected override void Start()
    {
        base.Start();
        Enemy owner = GetComponent<Enemy>();
        owner.RegisterAbility(this);

        mTarget = GameObject.FindGameObjectWithTag("Player");
        Rigidbody rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        //Debug.Log(mTarget.transform.position);
        if (MoveAudio != null && !MonsterAudio.isPlaying)
        {
            MonsterAudio.PlayOneShot(MoveAudio);
        }
        if (mTarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, mTarget.transform.position, step);
        }
    }
}
