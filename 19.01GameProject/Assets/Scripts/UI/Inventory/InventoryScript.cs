using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public int slotCount = 8;

    [SerializeField]
    private ItemIconVersion[] mItems;
    private Bag bags;

    [SerializeField]
    private Image mExplainImage;

    private static InventoryScript mInstance;
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

    public void AddItem(ItemIconVersion item)
    {
        if (item.MyStackSize > 0)
        {
            if (PlaceInStack(item))
            {
                return;
            }
        }
        PlaceInEmpty(item);

    }

    public void AddBag(Bag bag)
    {
        bags = bag;
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

    private bool PlaceInStack(ItemIconVersion item)
    {        
        foreach(SlotScript slots in bags.MyBagScript.MySlots)
        {
            if(slots.StackItem(item))
            {
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

    private void Awake()
    {
        // 가방을 생성하고
        Bag bag = (Bag)Instantiate(mItems[0]);

        // 가방의 슬롯 갯수를 정의하고
        bag.Initalize(slotCount);

        // 가방 아이템을 사용한다.
        bag.Use();
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            HealthPotion potion = (HealthPotion)Instantiate(mItems[1]);

            AddItem(potion);
        }
    }

}
