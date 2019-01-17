using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// 적, 몬스터 클래스
///</summary>
//
public class Enemy : Unit
{
    // Start is called before the first frame update
    void Start()
    {
        mIsActive = true;
        InitHP();
        this.gameObject.tag = "Enemy";
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
        {
            Die();
        }
    }

    protected override void Die() 
    {
        StartCoroutine("Respawn");

        SpawnCoin(this.transform.position);
        InActive();
        hp = max_hp;
    }

    void SpawnCoin(Vector3 pos)
    {
        List<GameObject> mCoins = GameObject.Find("CoinManager").GetComponent<ObjectPooling>().obejcts;

        for (int i = 0; i < mCoins.Count; i++)
        {
            if (mCoins[i].active == false)
            {
                mCoins[i].transform.position = pos;
                mCoins[i].SetActive(true);
                break;
            }
        }
    }
}
