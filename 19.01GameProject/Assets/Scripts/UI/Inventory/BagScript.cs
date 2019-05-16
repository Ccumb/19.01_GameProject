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
        InventoryScript.MyInstance.mSlotScripts = MySlots;
    }

    public bool AddItem(ItemIconVersion item)
    {
        if(item.name != "")
        {
            mSlots[item.slotPosition].AddItem(item);
            return true;
        }
        return false;
    }
}
