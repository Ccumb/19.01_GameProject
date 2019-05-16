using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotScript : MonoBehaviour, IClickable, IPointerClickHandler
{

    public bool IsMoveAble = false;
    private ObservableStack<ItemIconVersion> mItems = new ObservableStack<ItemIconVersion>();

    [SerializeField]
    private Image mIcon;

    [SerializeField]
    private Image mItemImage;

    [SerializeField]
    private Text mStackSize;

    public Text MyStackText
    {
        get
        {
            return mStackSize;
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

    public int MyCount
    {
        get
        {
            return mItems.Count;
        }
    }

    public ItemIconVersion MyItem
    {
        get
        {
            if(!IsEmpty)
            {
                return mItems.Peek();
            }

            return null;
        }
    }

    public ObservableStack<ItemIconVersion> MyItems
    {
        get
        {
            return mItems;
        }
    }

    public bool IsEmpty
    {
        get
        {
            return mItems.Count == 0;
        }
    }

    public bool AddItem(ItemIconVersion item)
    {
        if (item.MyStackSize > mItems.Count)
        {
            mItems.Push(item);
            mIcon.sprite = item.MyIcon;
            ///if문 붙이기 1/30일 작업할 것
            ///마우스 왼쪽 클릭 했을 때 오른쪽에 아이템 설명뜨게 하기!
            mItemImage.sprite = item.MyExplainItem;
            mIcon.color = Color.white;
            item.MySlot = this;
            return true;
        }
        return false;
    }

    public void RemoveItem(ItemIconVersion item)
    {
        // 자기 자신이 빈슬롯이 아니라면
        if (!IsEmpty)
        {
            // Items 의 제일 마지막 아이템을 꺼냅니다.
            UIManager.MyInstance.OnItemCountChanged(MyItems.Pop());
            
        }
    }

    public void UpdateSlot()
    {
        UIManager.MyInstance.UpdateStackSize(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
           
            InventoryScript.MyInstance.ChangeExplainImage(mItemImage.sprite);

            if(InventoryScript.MyInstance.FromSlot == null && !IsEmpty)
            {
                HandScript.MyInstance.objectname = MyItem.name;
                HandScript.MyInstance.TakeMoveable(MyItem as IMoveable);
                InventoryScript.MyInstance.FromSlot = this;
            }

            else if(InventoryScript.MyInstance.FromSlot != null)
            {
                if (IsMoveAble)
                {
                    if (IsPutItemBack() || IsSwapItems(InventoryScript.MyInstance.FromSlot) || IsAddItems(InventoryScript.MyInstance.FromSlot.mItems))
                    {
                        // Hand 오브젝트의 아이콘을 투명화와 MyMoveable을 null 로 변경하고
                        HandScript.MyInstance.Drop();
                        // FromSlot 을 null 로 변경합니다.
                        InventoryScript.MyInstance.FromSlot = null;
                    }
                }
            }
        }
        else
        {
            InventoryScript.MyInstance.ChangeExplainImage(null);
            HandScript.MyInstance.Drop();
        }
    }

    public void UseItem()
    {
        if (MyItem is IUseable)
        {
            // 해당 아이템을 사용한다.        
            (MyItem as IUseable).Use();
        }
    }

    public bool StackItem(ItemIconVersion item)
    {
        // 빈슬롯이 아니고
        // 해당 슬롯에 있는 아이템 이름과
        // 추가되려는 아이템의 이름이 동일하다면
        if (!IsEmpty && item.name == MyItem.name)
        {
            // 아이템의 중첩개수가
            // 아이템의 MyStackSize 보다 작다면
            if (mItems.Count < item.MyStackSize)
            {
                // 아이템을 중첩시킵니다.
                mItems.Push(item);
                item.MySlot = this;
                return true;
            }
        }        
        return false;
    }

    public bool IsFull
    {
        get
        {
            if(IsEmpty || MyCount < MyItem.MyStackSize)
            {
                return false;
            }
            else
            {                
                return true;
            }
        }
    }

    public bool IsAddItems(ObservableStack<ItemIconVersion> newItems)
    {
        // 해당 슬롯이 비어있거나 또는
        // newItems에 있는 아이템과 현재 슬롯의 아이템이 같다면
        if (IsEmpty || newItems.Peek().GetType() == MyItem.GetType())
        {
            int count = newItems.Count;
            ItemIconVersion tmpItem = newItems.Peek();
            for (int i = 0; i < count; i++)
            {
                // 슬롯이 가득 찼는지 확인
                if (IsFull)
                {
                    return false;
                }
                
                // 아이템을 추가하고 newItems의 리스트에서 삭제합니다.
                AddItem(newItems.Pop());
                //InventoryScript.MyInstance.FromSlot.AddItem(tmpItem);
            }
            return true;
        }
        return false;

    }


    public void UpdateStackSize(IClickable clickable)
    {
        if (clickable.MyCount > 1)
        {
            // 해당 슬롯의 중첩개수 표시하기
            clickable.MyStackText.text = clickable.MyCount.ToString();
            clickable.MyStackText.color = Color.white;
            clickable.MyIcon.color = Color.white;
        }

        else
        {
            // 해당 슬롯의 텍스트 투명하게 만들기
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
        }

        if (clickable.MyCount == 0)
        {
            // 해당 슬롯의 아이콘 투명하게 만들기
            clickable.MyIcon.color = new Color(0, 0, 0, 0);
            // 해당 슬롯의 텍스트 투명하게 만들기
            clickable.MyStackText.color = new Color(0, 0, 0, 0);

        }
    }

    private bool IsSwapItems(SlotScript from)
    {
        // 슬롯이 비어있다면
        if (IsEmpty)
        {
            return false;
        }

        // 동일한 아이템이 아니거나
        // 이동하려는 아이템 개수 + 현재 아이템 개수 가 아이템의 StackSize 보다 크다면
        if (from.MyItem.GetType() != MyItem.GetType() || from.MyCount + MyCount > MyItem.MyStackSize)
        {
            ObservableStack<ItemIconVersion> tmpFrom = new ObservableStack<ItemIconVersion>(from.mItems);

            // from 슬롯의 아이템 리스트를 초기화
            from.mItems.Clear();
            // from 슬롯의 아이템 리스트에 해당 슬롯의 아이템리스트 전달
            from.IsAddItems(mItems);

            // 현재 슬롯의 아이템 리스트 초기화
            mItems.Clear();

            // 현재 슬롯의 아이템 리스트를 tmpFrom 으로 변경
            IsAddItems(tmpFrom);
            
            return true;
        }

        return false;
    }

    private bool IsPutItemBack()
    {
        if(InventoryScript.MyInstance.FromSlot == this)
        {
            InventoryScript.MyInstance.FromSlot.MyIcon.color = Color.white;
            return true;
        }
        return false;
    }

    private void Awake()
    {
        mItems.OnPop += new UpdateStackEvent(UpdateSlot);
        mItems.OnPush += new UpdateStackEvent(UpdateSlot);
        mItems.OnClear += new UpdateStackEvent(UpdateSlot);
        mItems.OnPeek += new UpdateStackEvent(UpdateSlot);
    }
}
