using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillSlotScript : MonoBehaviour, IPointerClickHandler
{
    private Skill mSkill;

    [SerializeField]
    private Image mIcon;

    [SerializeField]
    private Image mItemImage;

    public bool IsEmpty
    {
        get
        {
            return mSkill == null;
        }
    }

    public Image MyIcon
    {
        get
        {
            return mIcon;
        }

        set
        {
            mIcon = value;
        }
    }

    public Skill MySkill
    {
        get
        {
            if (!IsEmpty)
            {
                return mSkill;
            }

            return null;
        }
    }


    public bool AddSkill(Skill skill)
    {       
        if (MySkill == null)
        {
            mSkill = skill;
            mIcon.sprite = skill.MyIcon;
            mItemImage.sprite = skill.MyExplainItem;
            mIcon.color = Color.white;
            skill.MySlot = this;
            return true;
        }
        return false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //인벤토리가 아니라 그 뭐시냐 스킬 세팅 스크립트에서 체인지 이미지를 해줘야 하지 않겄냐~ 이것이다 이말이여
            SkillSettingScript.MyInstance.ChangeExplainImage(mItemImage.sprite);
            if (SkillSettingScript.MyInstance.FromSlot == null && !IsEmpty)
            {
                HandScript.MyInstance.TakeMoveable(mSkill as IMoveable);
                HandScript.MyInstance.movingSkill = mSkill;
                HandScript.MyInstance.objectname = mSkill.name;
            }
            else
            {
                SkillSettingScript.MyInstance.ChangeExplainImage(null);
                HandScript.MyInstance.Drop();
            }
        }
        else
        {
            SkillSettingScript.MyInstance.ChangeExplainImage(null);
            HandScript.MyInstance.Drop();
        }
    }

    public void UseSkill()
    {
        if (MySkill is IUseable)
        {
            // 해당 아이템을 사용한다.        
            (MySkill as IUseable).Use();
        }
    }
    private bool IsPutItemBack()
    {
        if (InventoryScript.MyInstance.FromSlot == this)
        {
            InventoryScript.MyInstance.FromSlot.MyIcon.color = Color.white;
            return true;
        }
        return false;
    }
}
