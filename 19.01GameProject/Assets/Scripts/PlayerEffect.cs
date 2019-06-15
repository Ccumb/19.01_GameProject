using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;

public class PlayerEffect : MonoBehaviour
{
    public GameObject attackSkill;
    public GameObject heal;
    
    public Transform punchPos;
    public Transform healPos;
    
    private List<GameObject> mAttacks;
    private GameObject mHeal;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListeningCommonEvent("ActiveHeal", ActiveHeal);
        mAttacks = new List<GameObject>();

        for(int i = 0; i < 3; i++)
        {
            GameObject punch = Instantiate(attackSkill, punchPos);
            punch.SetActive(false);
            mAttacks.Add(punch);
        }

        mHeal = Instantiate(heal);
        mHeal.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        mHeal.transform.position = healPos.transform.position;
    }

    void SkillAttack()
    {
        for (int i = 0; i < mAttacks.Count; i++)
        {
            if (mAttacks[i].active == false)
            {
                mAttacks[i].active = true;
                break;
            }
        }
        StartCoroutine("AttackDelay");
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(1.8f);

        for (int i = 0; i < mAttacks.Count; i++)
        {
            if (mAttacks[i].active == true)
            {
                mAttacks[i].active = false;
                break;
            }
        }
    }

    void ActiveHeal()
    {
        mHeal.active = true;
        StartCoroutine("HealDelay");
    }

    IEnumerator HealDelay()
    {
        yield return new WaitForSeconds(3.0f);
        mHeal.active = false;
    }
}
