using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Bag", menuName = "Items/Bag", order = 1)]
public class Bag : ItemIconVersion, IUseable
{
    private int mSlots;

    [SerializeField]
    protected GameObject bagPrefab;

    public BagScript MyBagScript { get; set; }

    public int MyCost
    {
        get; set;
    }

    public int Slots
    {
        get
        {
            return mSlots;
        }
    }

    public void Initalize(int slots)
    {
        this.mSlots = slots; //Bag의 슬롯갯수 설정
    }

    public void Use()
    {
        MyBagScript = Instantiate(bagPrefab, InventoryScript.MyInstance.transform).GetComponent<BagScript>();

        MyBagScript.AddSlots(mSlots);

        InventoryScript.MyInstance.AddBag(this);
    }
}
