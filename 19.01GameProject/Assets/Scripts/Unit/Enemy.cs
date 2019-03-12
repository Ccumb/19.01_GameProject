using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// 적, 몬스터 클래스
///</summary>

[DisallowMultipleComponent]
public class Enemy : Unit
{
    private Vector3 mStartPos;
    private List<EnemyAbility> mAbilities;

    private void Awake()
    {
        mAbilities = new List<EnemyAbility>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        mbIsActive = true;
        InitHP();
        this.gameObject.tag = "Enemy";
        mStartPos = this.transform.position;
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
        hp = max_hp;

        DisableAbilities();
        SpawnCoin(this.transform.position);
        
        InActive();

        StartCoroutine("Respawn");
    }

    protected override void Active()
    {
        base.Active();
        
        AbleAbilities();
        this.transform.position = mStartPos;
    }

    void DisableAbilities()
    {
        for (int i = 0; i < mAbilities.Count; i++)
        {
            mAbilities[i].StopAllCoroutines();
            mAbilities[i].enabled = false;
        }
    }

    void AbleAbilities()
    {
        for (int i = 0; i < mAbilities.Count; i++)
        {
            mAbilities[i].enabled = true;
        }
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

    public void RegisterAbility(EnemyAbility ability)
    {
        mAbilities.Add(ability);
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        Active();
    }

}
