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

    [SerializeField]
    private List<ActionButton> mSkillQuickslots = new List<ActionButton>();

    [SerializeField]
    private Skill[] mSkillsList;

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
        mSkillListScript = GetComponentInChildren<SkillListScript>();

        mSkillListScript.AddSlots(slotCount);

        for(int i = 0; i < SkillQuickSlot.transform.childCount; i++)
        {
            mSkillQuickslots.Add(SkillQuickSlot.transform.GetChild(i).GetComponent<ActionButton>());
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            HealingSkill create = (HealingSkill)Instantiate(mSkillsList[0]);

            AddSkill(create);
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            PCoolTimeDownSkill create = (PCoolTimeDownSkill)Instantiate(mSkillsList[1]);

            AddSkill(create);
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            SkillBombAsset create = (SkillBombAsset)Instantiate(mSkillsList[2]);

            AddSkill(create);
        }

    }
}
