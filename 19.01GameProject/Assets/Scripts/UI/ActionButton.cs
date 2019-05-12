using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//HandScript에서 스킬도 변수를 추가해서 스킬을 클릭했을 경우에 같이 변수데이터 저장
//저장 후 여기서 이제 SetUseable 스킬 부분에다가 이벤트를 처리한다
//이벤트 쓰는 방법 헷갈리니까 그 Item Count부분 유심히 보고
//분석한 다음에 꼭 구현해보기 
//
//
//ActionButton에 등록한 아이템을 해지하는 방법도 찾자
public class ActionButton : MonoBehaviour, IPointerClickHandler, IClickable
{
    public string objectname = "Empty";

    [SerializeField]
    private Text mStackSize;

    [SerializeField]
    private Text mSkillCoolTime;

    [SerializeField]
    private Image mIcon;

    private Stack<IUseable> useables = new Stack<IUseable>();

    private int mCount;

    //Skill
    public IUseable MyUseable
    {
        get; set;
    }

    //Item
    public Stack<IUseable> MyUseables
    {
        get { return useables; }
        set { useables = value; }
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

    public int MyCount
    {
        get
        {
            return mCount;
        }
        set { mCount = value; }
    }

    public Text MyStackText
    {
        get
        {
            return mStackSize;
        }
        set
        {
            mStackSize = value;
        }
    }

    public Text MyCoolTimeText
    {
        get
        {
            return mSkillCoolTime;
        }
        set
        {
            mSkillCoolTime = value;
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
                useables.Peek().Use();
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
            if(!SkillSettingScript.MyInstance.IsCheckSkills(HandScript.MyInstance.objectname)
                || !RingCapacity.MyInstance.IsCheckRegistSkill(useable.MyCost))
            {
                HandScript.MyInstance.Drop();
                return;
            }
            // MyUseable.Use()는 버튼이 클릭되었을때 호출된다. 
            // MyUseable은 인터페이스로 Spell 에서 상속받고 있다.
            if(RingCapacity.MyInstance.IsCheckRegistSkill(useable.MyCost))
            {                
                SkillSettingScript.MyInstance.MyRingCount += useable.MyCost;
                this.MyUseable = useable;
                this.objectname = HandScript.MyInstance.objectname;
            }
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

        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(MyUseable != null && UIManager.MyInstance.chagneAvailable)
            {
                SkillSettingScript.MyInstance.MyRingCount -= MyUseable.MyCost;
                MyUseable = null;
                Clear();
            }
            else if(MyUseables != null && UIManager.MyInstance.chagneAvailable)
            {
                MyUseables = null;
                mCount = 0;
                MyStackText.text = "";
                Clear();
            }
        }
    }

    public void Clear()
    {
        MyIcon.sprite = null;
        MyIcon.color = new Color(0,0,0,0);
        objectname = "Empty";
    }

    public void UpdateVisual()
    {
        // ActionButton의 이미지를 변경한다.
        Sprite newd = HandScript.MyInstance.Put().MyIcon;
        MyIcon.sprite = newd;
        MyIcon.color = Color.white;

        if(mCount > 1)
        {
            UIManager.MyInstance.UpdateStackSize(this);
        }
    }
    
    public void UpdateItemCount(ItemIconVersion item)
    {
        if(item is IUseable && useables.Count > 0)
        {
            if(useables.Peek().GetType() == item.GetType())
            {
                useables = InventoryScript.MyInstance.GetUseables(item as IUseable);

                mCount = useables.Count;
                if(mCount == 0)
                    objectname = "Empty";
                UIManager.MyInstance.UpdateStackSize(this);
            }
        }
    }

    void Start()
    {
        MyButton = GetComponent<Button>();
        MyIcon = GetComponent<Image>();
        //MyButton.onClick.AddListener(OnClick);
        UIManager.MyInstance.itemCountChangedEvent += new ItemCountChanged(UpdateItemCount);
    }

    void OnEnable()
    {

    }
}
