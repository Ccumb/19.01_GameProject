using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Neremnem.Tools;
//현재 버그 :
//퀵슬롯에 처음 등록해서 아이템을 다 사용을 하면 멀쩡하게 연동이 되는데
//그 후에 포션을 다시 추가해서 사용을하면 아이템 슬롯에 있는 개수는 안줄어들고 퀵슬롯만 줄어든다
//구현해야할 사항 : 위에 버그고치고, 게임 플레이 창에서 퀵슬롯 아이템을 사용하고 인벤토리를 열었을 때 슬롯의 아이템과 동기화를 시키는 것
//
//
public class InventoryScript : MonoBehaviour
{  
    public GameObject ItemQuickSlot;
    public List<bool> own = new List<bool>();

    private List<ActionButton> mItemQuickSlots = new List<ActionButton>();

    [SerializeField]
    private ItemIconVersion[] mItems;

    [SerializeField]
    private Bag bags;
    
    [SerializeField]
    private Image mExplainImage;

    [SerializeField]
    public List<SlotScript> mSlotScripts = new List<SlotScript>();

    private SlotScript mFromSlot;
    private static InventoryScript mInstance;

    public int mGold;

    public static InventoryScript MyInstance
    {
        get
        {
            if(mInstance == null)
            {
                mInstance = FindObjectOfType<InventoryScript>();
            }
            return mInstance;
        }

        set
        {
            mInstance = value;
        }
    }

    public SlotScript FromSlot
    {
        get
        {
            return mFromSlot;
        }

        set
        {
            mFromSlot = value;
            if(value != null)
            {
                mFromSlot.MyIcon.color = Color.white;
            }
        }
    }

    public int Gold
    {
        get
        {
            return mGold;
        }
    }

    public void AddItem(ItemIconVersion item)
    {
        if (item.MyStackSize > 0)
        {
            if (IsPlaceInStack(item))
            {
                Debug.Log("add item " + item.name);
                return;
            }
        }
        PlaceInEmpty(item);
        Debug.Log("add item " + item.name);
    }

    public void Test()
    {
        own = new List<bool>();
        for(int i = 0; i < bags.MyBagScript.MySlots.Count; i++)
            own.Add(false);
        own = bags.MyBagScript.GetOwnItemList();
        for(int i = 0; i < own.Count; i++)
            Debug.Log(i + " = " + own[i]);
    }

    public void AddBag(Bag bag)
    {
        bags = bag;
    }

    //이거 나중에 따로 함수로 빼버리기
    public Stack<IUseable> GetUseables(IUseable type)
    {
        Stack<IUseable> useables = new Stack<IUseable>();

        // 가방퀵슬롯에 등록된 모든 가방을 검사.
        // 가방의 모든 슬롯을 검사
        foreach (SlotScript slot in bags.MyBagScript.MySlots)
        {
            // 빈슬롯이 아니고
            // 슬롯에 등록된 아이템이 type의 아이템과 같은 종류의 아이템이라면
            if (!slot.IsEmpty && slot.MyItem.GetType() == type.GetType())
            {
                // 해당 슬롯에 등록된 모든 아이템을
                foreach (ItemIconVersion item in slot.MyItems)
                {
                    // useables 에 담는다.                 
                        useables.Push(item as IUseable);
                }
            }
        }
        return useables;
    }

    public void ChangeExplainImage(Sprite sprite)
    {
        if(sprite == null)
        {
            mExplainImage.sprite = null;
            mExplainImage.color = new Color(0, 0, 0, 0);
            return;
        }
        mExplainImage.sprite = sprite;
        mExplainImage.color = Color.white;
    }

    public bool IsCheckItems(string name)
    {
        for (int i = 0; i < mItemQuickSlots.Count; i++)
        {
            if (mItemQuickSlots[i].objectname == name)
            {
                return false;
            }
        }
        return true;
    }

    public List<ActionButton> GetInventoryQuickSlotList()
    {
        return mItemQuickSlots;
    }

    public void SetInveontryQuickSlotList(List<ActionButton> action)
    {
        for(int i = 0; i < action.Count; i ++)
        {
            mItemQuickSlots[i] = action[i];
        }
    }

    public void UpdateGold(int amount)
    {
        mGold += amount;
    }

    public bool ItemIsFull(string itemName)
    {
        foreach (SlotScript slots in bags.MyBagScript.MySlots)
        {
            if(slots.MyItem != null)
            {
                if(slots.MyItem.name == (itemName +"(Clone)"))
                {                    
                    return slots.IsFull;
                }
            }
        }
        return false;
    }

    private bool IsPlaceInStack(ItemIconVersion item)
    {                
        foreach(SlotScript slots in mSlotScripts)
        {         
            if(slots.StackItem(item))
            {
                UIManager.MyInstance.OnItemCountChanged(item);
                return true;
            }
        }
        return false;
    }

    private void PlaceInEmpty(ItemIconVersion item)
    {
        if (bags.MyBagScript.AddItem(item))
        {
            return;
        }
    }
    
    private void Start()
    {
        for (int i = 0; i < ItemQuickSlot.transform.childCount; i++)
        {
            mItemQuickSlots.Add(ItemQuickSlot.transform.GetChild(i).GetComponent<ActionButton>());
        }
      
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            HealthPotion potion = (HealthPotion)Instantiate(mItems[1]);

            AddItem(potion);
        }
    }

    //현재 itemlist에는 bag도 포함되어 있는데 이걸 따로 변수로 빼놓는게 더 편할 수도 있을 것 같으니 빼놓자..
    private void LoadItemList(int i)
    {
        switch(i)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                HealthPotion potion = (HealthPotion)Instantiate(mItems[2]);

                AddItem(potion);
                break;
        }
    }

    private void LoadItems()
    {
        for(int i = 0; i < own.Count; i++)
        {
            if(own[i])
                LoadItemList(i);
        }
    }

}
