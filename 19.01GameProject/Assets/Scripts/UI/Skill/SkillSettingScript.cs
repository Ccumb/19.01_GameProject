using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSettingScript : MonoBehaviour
{
    public int slotCount = 8;

    public GameObject SkillQuickSlot;

    private List<ActionButton> mSkillQuickslots = new List<ActionButton>();

    [SerializeField]
    private Skill[] mSkills;

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
            CreateSkill create = (CreateSkill)Instantiate(mSkills[0]);

            AddSkill(create);
        }
    }
}
