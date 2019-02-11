using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotScript : MonoBehaviour, IClickable, IPointerClickHandler
{
    private ObservableStack<ItemIconVersion> mItems = new ObservableStack<ItemIconVersion>();

    [SerializeField]
    private Image mIcon;

    [SerializeField]
    private Image mItemImage;

    [SerializeField]
    private Text mStackSize;

    public bool IsEmpty
    {
        get
        {
            return mItems.Count == 0;
        }
    }

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


    public bool AddItem(ItemIconVersion item)
    {
        if (mItems.Count == 0)
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
            mItems.Pop();

            //UpdateStackSize(this);
        }
    }

    public void UpdateSlot()
    {
        UpdateStackSize(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {                       
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            UseItem();
        }

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            InventoryScript.MyInstance.ChangeExplainImage(mItemImage.sprite);
        }
        else
        {
            InventoryScript.MyInstance.ChangeExplainImage(null);
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

    private void Awake()
    {
        mItems.OnPop += new UpdateStackEvent(UpdateSlot);
        mItems.OnPush += new UpdateStackEvent(UpdateSlot);
        mItems.OnClear += new UpdateStackEvent(UpdateSlot);
    }
}
