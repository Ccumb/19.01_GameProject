using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagScript : MonoBehaviour
{
    [SerializeField]
    private GameObject mSlotPrefab;

    private List<SlotScript> mSlots = new List<SlotScript>();

    private List<bool> own = new List<bool>();

    public List<SlotScript> MySlots
    {
        get
        {
            return mSlots;
        }
    }

    public List<bool> MyOwn
    {
        get
        {
            return own;
        }
    }

    public List<bool> GetOwnItemList()
    {
        for(int i = 0; i < MySlots.Count; i++)
        {
            if(!MySlots[i].IsEmpty)
                own[i] = true;
            else
                own[i] = false;
        }
        return own;
    }

    public void AddSlots(int slotCount)
    {
        for(int i = 0; i < slotCount; i++)
        {
            SlotScript slot = Instantiate(mSlotPrefab, transform).GetComponent<SlotScript>();
            mSlots.Add(slot);
            own.Add(false);
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
