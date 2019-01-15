using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// 플레이어 캐릭터 클래스
///</summary>
public class Player : Unit
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.tag = "Player";
        InitHP();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            Die();
        }
    }
}
