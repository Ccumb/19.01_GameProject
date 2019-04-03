using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform RespawnPoint = null; //리스폰 위치
    public float RespawnTime = 30.0f; //리스폰 될 시간

    private Enemy mEnemy = null; // Die변수를 가져오기 위한 script변수
    private float mRespawnTime = 0.0f; //지나가는 시간

    /// <summary>
    /// Start에서 Enemy스크립트를 받아오는 것, bool 변수 Die를 알아야 하기 때문
    /// </summary>
    void Start()
    {
        mEnemy = transform.GetChild(0).GetComponent<Enemy>();
    }

    /// <summary>
    /// ActiveSelf의 여부와 상관없이 Die가 True라면 스폰 함수를 돌리기 시작함
    /// </summary>
    void Update()
    {
        if(mEnemy.bDie)
        {
            mRespawnTime += Time.deltaTime;
            if(mRespawnTime > RespawnTime)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).gameObject.transform.position = RespawnPoint.position;
                mRespawnTime = 0.0f;
            }
        }
    }
}
