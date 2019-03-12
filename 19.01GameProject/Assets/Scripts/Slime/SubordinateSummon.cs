using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SubordinatePool))]
[DisallowMultipleComponent]
public class SubordinateSummon : EnemyAbility
{
    public int SummonCount = 3;
    public float cAccumulateSummonTime = 0.0f;
    public float cSummonTime = 10.0f;
    public Transform[] tSummonTransform = new Transform[5];

    private GameObject mTarget;
    private bool bSummon = false;
    private int iFalseCount = 0;

    SubordinatePool SummonPool = null;

    void Start()
    {
        SummonPool = GetComponent<SubordinatePool>();
        mTarget = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.forward = new Vector3((mTarget.transform.position - transform.position).normalized.x, 0, (mTarget.transform.position - transform.position).normalized.z);
        iFalseCount = 0;
        for (int i = 0; i < transform.parent.GetChild(1).childCount; i++)
        {
            if (!transform.parent.GetChild(1).GetChild(i).gameObject.activeSelf)
            {
                iFalseCount++;
            }
        }
        if(iFalseCount == transform.parent.GetChild(1).childCount)
        {
            bSummon = true;
        }

        if (bSummon)
        {
            cAccumulateSummonTime += Time.deltaTime;
            if (cAccumulateSummonTime > cSummonTime)
            {
                SummonPool.Pooling(SummonCount, tSummonTransform);
                if (SummonCount < 5)
                {
                    SummonCount++;
                }
                cAccumulateSummonTime = 0.0f;
                bSummon = false;
            }
        }
    }
}
