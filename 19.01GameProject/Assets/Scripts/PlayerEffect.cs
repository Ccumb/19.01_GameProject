using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    public GameObject footStep;
    public GameObject attackSkill;

    public Transform footPos;
    public Transform punchPos;

    private List<GameObject> mFootSteps;
    private List<GameObject> mAttacks;

    // Start is called before the first frame update
    void Start()
    {
        mFootSteps = new List<GameObject>();
        mAttacks = new List<GameObject>();

        for (int i = 0; i < 10; i++)
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
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
        StartCoroutine("FootDelay");
    }

    IEnumerator FootDelay()
    {
        yield return new WaitForSeconds(2.0f);

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
}
