using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public delegate void RingCountChanged(int count);
public class SkillSettingScript : MonoBehaviour
{
    public event RingCountChanged RingCountchange;
    public int slotCount = 8;
    public int tmpRingCount = 0;
    public GameObject SkillQuickSlot;
    public List<bool> own = new List<bool>();

    [SerializeField]
    private List<ActionButton> mSkillQuickslots = new List<ActionButton>();

    [SerializeField]
    private Skill[] mSkillsList;

    [SerializeField]
    private GameObject mList;

    private SkillListScript mSkillListScript;


    [SerializeField]
    private Image mExplainImage;

    private SkillSlotScript mFromSlot;
    private static SkillSettingScript mInstance;

    public static SkillSettingScript MyInstance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = FindObjectOfType<SkillSettingScript>();
            }
            return mInstance;
        }
        
        set
        {
            mInstance = value;
        }
    }

    public int MyRingCount
    {
        get { return tmpRingCount; }
        set
        {
            tmpRingCount = value;
            RingCountchange.Invoke(tmpRingCount);
        }
    }

    public List<ActionButton> MySkillList
    {
        get { return mSkillQuickslots; }
    }
    public SkillSlotScript FromSlot
    {
        get
        {
            return mFromSlot;
        }

        set
        {
            mFromSlot = value;
            if (value != null)
            {
                mFromSlot.MyIcon.color = Color.white;
            }
        }
    }

    public void AddSkill(Skill skill)
    {
        if(mSkillListScript.AddSkill(skill))
        {
            return;
        }
    }

    //SkillListScript에서 가지고 있는 스킬들(bool)을 가져온다.
    //임시로 Test라고 지어놓음. 
    public void Test()
    {
        own = new List<bool>();
        for(int i = 0; i < mSkillListScript.MySlots.Count; i++)
            own.Add(false);
        own = mSkillListScript.GetOwnSkillList();
        for(int i = 0; i < own.Count; i++)
            Debug.Log(i + " = " + own[i]);
    }

    public void ChangeExplainImage(Sprite sprite)
    {
        if (sprite == null)
        {
            mExplainImage.sprite = null;
            mExplainImage.color = new Color(0, 0, 0, 0);
            return;
        }
        mExplainImage.sprite = sprite;
        mExplainImage.color = Color.white;
    }

    public bool IsCheckSkills(string name)
    {
        for (int i = 0; i < mSkillQuickslots.Count; i++)
        {
            if (mSkillQuickslots[i].objectname == name)
            {               
                return false;
            }
        }
        return true;
    }

    private void Start()
    {
        mSkillListScript = mList.GetComponent<SkillListScript>();

        mSkillListScript.AddSlots(slotCount);

        for(int i = 0; i < SkillQuickSlot.transform.childCount; i++)
        {
            mSkillQuickslots.Add(SkillQuickSlot.transform.GetChild(i).GetComponent<ActionButton>());
        }

        LoadSkills();
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.L))
        //{
        //    HealingSkill create = (HealingSkill)Instantiate(mSkillsList[0]);

        //    AddSkill(create);
        //}
        //if(Input.GetKeyDown(KeyCode.J))
        //{
        //    SkillBombAsset create1 = (SkillBombAsset)Instantiate(mSkillsList[1]);

        //    AddSkill(create1);
        //}
        //if(Input.GetKeyDown(KeyCode.J))
        //{
        //    ActiveSkillShortDistanceAttackAsset create2 = (ActiveSkillShortDistanceAttackAsset)Instantiate(mSkillsList[2]);

        //    AddSkill(create2);
        //}
        //if(Input.GetKeyDown(KeyCode.J))
        //{
        //    ActiveSkillLongDistanceAttackAsset create3 = (ActiveSkillLongDistanceAttackAsset)Instantiate(mSkillsList[3]);

        //    AddSkill(create3);
        //}
        //if(Input.GetKeyDown(KeyCode.J))
        //{
        //    PCoolTimeDownSkill create4 = (PCoolTimeDownSkill)Instantiate(mSkillsList[4]);

        //    AddSkill(create4);
        //}
        //if(Input.GetKeyDown(KeyCode.J))
        //{
        //    PassiveHeal create5 = (PassiveHeal)Instantiate(mSkillsList[5]);

        //    AddSkill(create5);
        //}
        //if(Input.GetKeyDown(KeyCode.J))
        //{
        //    BasicAttackPowerIncrease create7 = (BasicAttackPowerIncrease)Instantiate(mSkillsList[7]);

        //    AddSkill(create7);
        //}
        //if(Input.GetKeyDown(KeyCode.O))
        //{
        //    Test();
        //}
    }

    //스킬들의 slotposition과 mSkillList의 인덱스값을 일치 시켜놓은거라 추후 수정이 되면 동기화 작업 필요  
    private void LoadSkillList(int i )
    {
        switch(i)
        {
            case 0:
                HealingSkill create = (HealingSkill)Instantiate(mSkillsList[0]);

                AddSkill(create);
                break;
            case 1:
                SkillBombAsset create1 = (SkillBombAsset)Instantiate(mSkillsList[1]);

                AddSkill(create1);
                break;
            case 2:
                ActiveSkillShortDistanceAttackAsset create2 = (ActiveSkillShortDistanceAttackAsset)Instantiate(mSkillsList[2]);

                AddSkill(create2);
                break;
            case 3:
                ActiveSkillLongDistanceAttackAsset create3 = (ActiveSkillLongDistanceAttackAsset)Instantiate(mSkillsList[3]);

                AddSkill(create3);
                break;
            case 4:
                PCoolTimeDownSkill create4 = (PCoolTimeDownSkill)Instantiate(mSkillsList[4]);

                AddSkill(create4);
                break;
            case 5:
                PassiveHeal create5 = (PassiveHeal)Instantiate(mSkillsList[5]);

                AddSkill(create5);
                break;
            case 6:
                SkillEffectIncrease create6 = (SkillEffectIncrease)Instantiate(mSkillsList[6]);

                AddSkill(create6);
                break;
            case 7:
                BasicAttackPowerIncrease create7 = (BasicAttackPowerIncrease)Instantiate(mSkillsList[7]);

                AddSkill(create7);
                break;
        }
    }

    //List<bool>own 의 값이 true이면 LoadSkillList함수를 통해 해당하는 스킬들을 슬롯에다가 넣어준다.
    private void LoadSkills()
    {
        for(int i = 0; i < own.Count; i++)
        {
            if(own[i])
                LoadSkillList(i);
        }
    }
}
