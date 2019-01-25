using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowEnemyHP : MonoBehaviour
{
    private Image mHealthBar;

    private Text mBossName;

    private float mHp;
    private float mMax_hp;

    private void Start()
    {
        mHealthBar = GetComponent<Image>();
        mMax_hp = GameObject.Find("Enemy").GetComponent<Enemy>().max_hp; // Boss 오브젝트의 이름이 바뀌면 이것도 바꿔줘야 한다
        mBossName = GetComponentInChildren<Text>(); // HP바 자식으로 보스의 이름이 들어가 있는데 다른 방법으로 바꿔줘야 할듯.. 임시방편이다.
    }

    // Update is called once per frame
    void Update()
    {
        mHp = GameObject.Find("Enemy").GetComponent<Enemy>().hp;  
        string bossName = "BossName";
        mBossName.text = bossName;
        mHealthBar.fillAmount = mHp / mMax_hp;
    }
}
