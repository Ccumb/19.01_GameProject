using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowEnemyHP : MonoBehaviour
{
    [SerializeField]
    private GameObject mUIBar;

    private Image mHealthBar;

    private Text mBossName;

    [SerializeField]
    private float mHp;
    private float mMax_hp;

    private void Start()
    {
        mHealthBar = GetComponent<Image>();
        mBossName = GetComponentInChildren<Text>(); // HP바 자식으로 보스의 이름이 들어가 있는데 다른 방법으로 바꿔줘야 할듯.. 임시방편이다.
        string bossName = "BossName";
        mBossName.text = bossName;

        if(GameObject.FindGameObjectWithTag("Enemy"))
        {
            mMax_hp = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>().max_hp;
            StartCoroutine("CheckHP");
        }
        else
        {
            Invoke("FindEnemy", 0.02f);
        }
    }

    void FindEnemy()
    {
        if (GameObject.FindGameObjectWithTag("Enemy"))
        {
            mBossName.text = GameObject.FindGameObjectWithTag("Enemy").name;
            mMax_hp = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>().max_hp;
            StartCoroutine("CheckHP");
        }
    }

    IEnumerator CheckHP()
    {
        yield return new WaitForSeconds(0.1f);
        while(true)
        {
            if(!GameObject.FindGameObjectWithTag("Enemy"))
            {
                break;
            }
            mHp = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>().hp;
            mHealthBar.fillAmount = mHp / mMax_hp;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
