using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// 적, 몬스터 클래스
///</summary>
public class Enemy : Unit
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.tag = "Enemy";
        InitHP();
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
        {
            Die();
        }
    }
}
