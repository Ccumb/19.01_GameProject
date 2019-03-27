using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    private List<ItemIconVersion> mItemList = new List<ItemIconVersion>();            // 아이템 리스트

    private List<ItemIconVersion> mRandomItemList = new List<ItemIconVersion>();      // 랜덤으로 갱신될 아이템 리스트
    private List<ItemIconVersion> mFixedItemList = new List<ItemIconVersion>();       // 고정으로 판대될 아이템 리스트

    private Stack<int> mPrevItemIndexList = new Stack<int>();                         // 이전 아이템 인덱스 번호 스택

    private List<ItemForShop> mRandomItemsPlaceList = new List<ItemForShop>();   // 랜덤으로 갱신될 아이템들이 위치될 좌표에 대한 리스트
    private List<ItemForShop> mFixedItemsPlaceList = new List<ItemForShop>();    // 고정으로 판매될 아이템들이 위치될 좌표에 대한 리스트

    public float updateTime;                            // 아이템 리스트 갱신 시간

    public int randomItemNum;                           // 랜덤 아이템 팔 갯수

    public IEnumerator UpdateRandomItemList()            // 랜덤 아이템 리스트 갱신
    {

        yield return new WaitForSeconds(updateTime);
    }
    

    private void InitItemLists()                        // 아이템 리스트들에 대한 초기화
    {
        object[] randomItems = Resources.LoadAll("Items/Random");

        for (int i = 0; i < randomItems.Length; i++)
        {
            ItemIconVersion item = randomItems[i] as ItemIconVersion;
            mItemList.Add(item);
        }

        object[] fixedItems = Resources.LoadAll("Items/Fixed");

        for (int i = 0; i < fixedItems.Length; i++)
        {
            ItemIconVersion item = fixedItems[i] as ItemIconVersion;
            mFixedItemList.Add(item);
        }
    }

    private void InitFixedItemPlaces()                       // 아이템을 놓는 자리들에 대한 초기화, 고정 아이템용
    {
        ItemForShop[] fixedItemsPlaceList = GetComponentsInChildren<ItemForShop>();

        for(int i = 0; i < fixedItemsPlaceList.Length; i++)
        {
            if(fixedItemsPlaceList[i].tag == "ForFixed")
            {
                mFixedItemsPlaceList.Add(fixedItemsPlaceList[i]);
                fixedItemsPlaceList[i].MyItemInfo = mFixedItemList[i];
                fixedItemsPlaceList[i].Renderer.material.SetTexture("_MainTex", mFixedItemList[i].MyIcon.texture);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitItemLists();
        InitFixedItemPlaces();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
