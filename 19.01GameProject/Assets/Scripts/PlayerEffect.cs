using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;

public class PlayerEffect : MonoBehaviour
{
    public GameObject footStep;
    public GameObject attackSkill;
    public GameObject heal;

    public Transform footPos;
    public Transform punchPos;
    public Transform healPos;

    private List<GameObject> mFootSteps;
    private List<GameObject> mAttacks;
    private GameObject mHeal;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListeningCommonEvent("ActiveHeal", ActiveHeal);
        mFootSteps = new List<GameObject>();
        mAttacks = new List<GameObject>();

        for (int i = 0; i < 50; i++)
        {
            GameObject foot = Instantiate(footStep);
            foot.SetActive(false);
            mFootSteps.Add(foot);
        }

        for(int i = 0; i < 3; i++)
        {
            GameObject punch = Instantiate(attackSkill, punchPos);
            punch.SetActive(false);
            mAttacks.Add(punch);
        }

        mHeal = Instantiate(heal);
        mHeal.SetActive(false);

        Debug.Log(mFootSteps.Count);
    }

    // Update is called once per frame
    void Update()
    {
        mHeal.transform.position = healPos.transform.position;
    }

    void FootStep()
    {
        for(int i = 0; i < mFootSteps.Count; i++)
        {
            if(mFootSteps[i].active == false)
            {
                mFootSteps[i].transform.position = footPos.transform.position;
                mFootSteps[i].active = true;
                break;
            }
            StartCoroutine("FootDelay");
        }
    }

    IEnumerator FootDelay()
    {
        yield return new WaitForSeconds(2.5f);

        for (int i = 0; i < mFootSteps.Count; i++)
        {
            if (mFootSteps[i].active == true)
            {
                mFootSteps[i].active = false;
                break;
            }
        }
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

        for (int i = 0; i < mFootSteps.Count; i++)
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
