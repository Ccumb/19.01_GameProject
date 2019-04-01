using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform RespawnPoint = null;
    public float RespawnTime = 30.0f;

    Enemy mEnemy = null;
    private float mTime = 0.0f;

    void Start()
    {
        mEnemy = transform.GetChild(0).GetComponent<Enemy>();
    }

    void Update()
    {
        if(mEnemy.bDie)
        {
            mTime += Time.deltaTime;
            if(mTime > RespawnTime)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).gameObject.transform.position = RespawnPoint.position;
            }
        }
    }
}
