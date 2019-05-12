using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBag : MonoBehaviour
{
    public int slotCount = 0;
    [SerializeField]
    private ItemIconVersion bagAsset;
    // Start is called before the first frame update
    void Awake()
    {
        // 가방을 생성하고
        Bag bag = (Bag)Instantiate(bagAsset,GameObject.Find("InventorySlot").transform);
        // 가방의 슬롯 갯수를 정의하고       
        bag.Initalize(slotCount);
        // 가방 아이템을 사용한다.
        bag.Use();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
