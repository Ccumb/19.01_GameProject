using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// 플레이어 캐릭터 클래스
///</summary>
public class Playera : Unit
{
    private int mGold;
    private List<CharacterAbility> mAbilities;

    private void Awake()
    {
        mAbilities = new List<CharacterAbility>();
    }

    public void RegisterAbility(CharacterAbility ability)
    {
        mAbilities.Add(ability);
    }

    // Start is called before the first frame update
    void Start()
    {
        mbIsActive = true;
        this.gameObject.tag = "Player";
        //InitHP();
        mGold = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            hp = 0;
            Die();
        }
    }

    /// <summary>
    /// 일단 사후처리가 애매하므로, 움직임, 공격(등의 능력사용)만 불가능하게 처리함.
    /// </summary>
    protected override void Die()
    {
        DisableAbilities();
    }

    public void UpdateGold(int gold)
    {
        mGold += gold;
    }

    public int Gold
    {
        get
        {
            return mGold;
        }
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
}
