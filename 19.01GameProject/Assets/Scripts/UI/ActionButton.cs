using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//ActionButton에 등록한 아이템을 해지하는 방법도 찾자
public class ActionButton : MonoBehaviour, IPointerClickHandler
{
    public string objectname;

    [SerializeField]
    private Image mIcon;

    private Stack<IUseable> useables;

    private int mCount;

    public IUseable MyUseable
    {
        get; set;
    }

    public Button MyButton
    {
        get;
        private set;
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

    public void OnClick()
    {
        if (HandScript.MyInstance.MyMoveable == null)
        {
            // 액션퀵슬롯에 등록된 것이 스킬이라면
            if (MyUseable != null)
            {
                MyUseable.Use();
            }


            // 액션퀵슬롯에 사용가능한 아이템이 등록되었고
            // 등록된 아이템의 개수가 1개 이상이라면
            if (useables != null && useables.Count > 0)
            {
                // useables 배열에서 개체 하나를 삭제하고
                // 삭제된 녀석을 Use 기능을 사용한다.
                useables.Pop().Use();
            }
        }
    }

    public void SetUseable(IUseable useable)
    {
        // 액션 퀵슬롯에 등록되려는 것이 아이템이라면
        if (useable is ItemIconVersion)
        {
            if (!InventoryScript.MyInstance.IsCheckItems(HandScript.MyInstance.objectname))
            {
                HandScript.MyInstance.Drop();
                InventoryScript.MyInstance.FromSlot.MyIcon.color = Color.white;
                InventoryScript.MyInstance.FromSlot = null;
                return;
            }
            InventoryScript.MyInstance.FromSlot.MyIcon.color = Color.white;
            InventoryScript.MyInstance.FromSlot = null;
            // 해당 아이템과 같은 종류의 아이템을 가진 리스트를 저장하고
            useables = InventoryScript.MyInstance.GetUseables(useable);
            // 개수 저장
            mCount = useables.Count;
            this.objectname = HandScript.MyInstance.objectname;
            //  이동모드 상태 해제

        }
        else
        {
            if(!SkillSettingScript.MyInstance.IsCheckSkills(HandScript.MyInstance.objectname))
            {
                HandScript.MyInstance.Drop();
                return;
            }
            // MyUseable.Use()는 버튼이 클릭되었을때 호출된다. 
            // MyUseable은 인터페이스로 Spell 에서 상속받고 있다.
            this.MyUseable = useable;
            this.objectname = HandScript.MyInstance.objectname;
        }
        
        UpdateVisual();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (HandScript.MyInstance.MyMoveable != null)
            {
                // IUseable 로 변환할 수 있는지 확인.
                if (HandScript.MyInstance.MyMoveable is IUseable)
                {
                    SetUseable(HandScript.MyInstance.MyMoveable as IUseable);
                }
            }
        }
    }

    public void UpdateVisual()
    {
        // ActionButton의 이미지를 변경한다.
        Sprite newd = HandScript.MyInstance.Put().MyIcon;
        MyIcon.sprite = newd;
        MyIcon.color = Color.white;
    }

    void Start()
    {
        MyButton = GetComponent<Button>();
        MyIcon = GetComponent<Image>();
        //MyButton.onClick.AddListener(OnClick);
    }

}
