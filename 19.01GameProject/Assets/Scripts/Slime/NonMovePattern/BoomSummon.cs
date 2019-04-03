using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SubordinatePool))]
[DisallowMultipleComponent]
public class BoomSummon : EnemyAbility
{
    public float SpawnDelayTime = 10.0f;
    public Transform SpawnPoint = null;
    public int SummonCount = 1;

    private Transform[] aSpawnPoint = null;
    public float mSpanwTime = 0.0f;
    private SubordinatePool mSummonPool = null;

    private void Awake()
    {
        mSummonPool = GetComponent<SubordinatePool>();
        aSpawnPoint = new Transform[mSummonPool.ObjectCount];
        for (int i = 0; i < mSummonPool.ObjectCount; i++)
        {
            aSpawnPoint[i] = SpawnPoint;
        }
    }
    void Update()
    {
        mSpanwTime += Time.deltaTime;
        if (mSpanwTime > SpawnDelayTime)
        {
            mSummonPool.Pooling(SummonCount, aSpawnPoint);
            mSpanwTime = 0.0f;
        }
    }
}
