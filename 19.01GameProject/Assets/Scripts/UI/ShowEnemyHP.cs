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
<<<<<<< HEAD

        mMax_hp = GameObject.FindGameObjectWithTag("Monster").GetComponent<Enemy>().max_hp;
        if(GameObject.FindGameObjectWithTag("Monster"))
        {
            Debug.Log("fIND");
            mMax_hp = GameObject.FindGameObjectWithTag("Monster").GetComponent<Enemy>().max_hp;
=======
        Debug.Log(GameObject.FindGameObjectWithTag("Enemy"));
        if(GameObject.FindGameObjectWithTag("Enemy"))
        {
            mMax_hp = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>().max_hp;
>>>>>>> 1c8ffa8acc7ccb15e7d3167dbfc8050642cddc74
            StartCoroutine("CheckHP");
        }
        else
        {
            mUIBar.SetActive(false);
        }
    }

    IEnumerator CheckHP()
    {
        yield return new WaitForSeconds(0.1f);
        while(true)
        {
            if(!GameObject.FindGameObjectWithTag("Monster"))
            {
                break;
            }
            mHp = GameObject.FindGameObjectWithTag("Monster").GetComponent<Enemy>().hp;
            mHealthBar.fillAmount = mHp / mMax_hp;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
