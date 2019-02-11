using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagScript : MonoBehaviour
{
    [SerializeField]
    private GameObject mSlotPrefab;

    private List<SlotScript> mSlots = new List<SlotScript>();

    public List<SlotScript> MySlots
    {
        get
        {
            return mSlots;
        }
    }

    public void AddSlots(int slotCount)
    {
        for(int i = 0; i < slotCount; i++)
        {
            SlotScript slot = Instantiate(mSlotPrefab, transform).GetComponent<SlotScript>();
            mSlots.Add(slot);
        }
    }

    public bool AddItem(ItemIconVersion item)
    {
        if(item.name == "HealthPotion(Clone)")
        {
            mSlots[item.slotPosition].AddItem(item);
            return true;
        }
        /*foreach (SlotScript slot in mSlots)
        {            
            // 빈 슬롯이 있으면
            if (slot.IsEmpty)
            {
                // 해당 슬롯에 아이템을 추가한다.
                slot.AddItem(item);
                return true;
            }
        }*/

        return false;
    }
}
