using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SubordinatePool))]
[DisallowMultipleComponent]
public class SubordinateSummon : EnemyAbility
{
    public int SummonCount = 3;
    public float SummonTime = 10.0f;
    public Transform[] SummonTransform = new Transform[5];

    public ParticleSystem Explosion = null;
    public float ParticleScale = 10.0f;

    private float mSummonTime = 0.0f;
    private bool bSummon = false;
    private int FalseCount = 0;

    SubordinatePool SummonPool = null;

    private void OnEnable()
    {
        SummonCount = 3;
        mSummonTime = 0.0f;
        bSummon = false;
    }

    private void Awake()
    {
        SummonPool = GetComponent<SubordinatePool>();
    }

    void Update()
    {
        FalseCount = 0;
        for (int i = 0; i < transform.parent.GetChild(1).childCount; i++)
        {
            if (!transform.parent.GetChild(1).GetChild(i).gameObject.activeSelf)
            {
                FalseCount++;
            }
        }

        if(FalseCount == transform.parent.GetChild(1).childCount)
        {
            bSummon = true;
        }

        if (bSummon)
        {
            mSummonTime += Time.deltaTime;
            if (mSummonTime > SummonTime)
            {
                Explosion.transform.localScale = new Vector3(ParticleScale, ParticleScale, ParticleScale);
                Instantiate(Explosion, transform.position, Quaternion.identity);
                SummonPool.Pooling(SummonCount, SummonTransform);
                if (SummonCount < 5)
                {
                    SummonCount++;
                }
                mSummonTime = 0.0f;
                bSummon = false;
            }
        }
    }
}
