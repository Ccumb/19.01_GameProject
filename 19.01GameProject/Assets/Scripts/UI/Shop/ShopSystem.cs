using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    private List<ItemIconVersion> mItemList = new List<ItemIconVersion>();            // 아이템 리스트

    private List<ItemIconVersion> mRandomItemList = new List<ItemIconVersion>();      // 랜덤으로 갱신될 아이템 리스트
    private List<ItemIconVersion> mFixedItemList = new List<ItemIconVersion>();       // 고정으로 판대될 아이템 리스트

    private Queue<int> mRandomIndexQueue = new Queue<int>();                         // 아이템 인덱스 번호 Queue
    private List<int> mIndexList = new List<int>();

    private List<ItemForShop> mRandomItemsPlaceList = new List<ItemForShop>();   // 랜덤으로 갱신될 아이템들이 위치될 좌표에 대한 리스트
    private List<ItemForShop> mFixedItemsPlaceList = new List<ItemForShop>();    // 고정으로 판매될 아이템들이 위치될 좌표에 대한 리스트

    public float updateTime;                            // 아이템 리스트 갱신 시간

    public IEnumerator UpdateRandomItemList()            // 랜덤 아이템 리스트 갱신
    {
        while(true)
        {
            for (int i = 0; i < mRandomItemsPlaceList.Count; i++)
            {
                if (mRandomIndexQueue.Count == 0)
                {
                    MakeRandomIndexQueue();
                }

                int n = mRandomIndexQueue.Dequeue();

                mRandomItemList[i] = mItemList[n];
            }

            UpdateRandomItemPlace();

            yield return new WaitForSeconds(updateTime);
        }
    }

    private void ShuffleIndexList()
    {
        if (mIndexList.Count != 0)            // 리스트에 뭔가라도 있다면
        {
            int random1 = 0;
            int random2 = 0;

            int tmp = 0;

            for (int i = 0; i < mIndexList.Count; i++)
            {
                random1 = UnityEngine.Random.Range(0, mIndexList.Count);
                random2 = UnityEngine.Random.Range(0, mIndexList.Count);

                tmp = mIndexList[random1];
                mIndexList[random1] = mIndexList[random2];
                mIndexList[random2] = tmp;
            }
        }
    }


    private void MakeRandomIndexQueue()
    {
        mRandomIndexQueue.Clear();          // 스택을 비우고

        ShuffleIndexList();                 // 인덱스 리스트를 섞고

        for(int i = 0; i < mIndexList.Count; i++)
        {
            mRandomIndexQueue.Enqueue(mIndexList[i]);
        }
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

        for (int i = 0; i < mItemList.Count; i++)
        {
            mIndexList.Add(i);
        }
    }

    private void InitFixedItemPlaces()                       // 아이템을 놓는 자리들에 대한 초기화, 고정 아이템용
    {
        ItemForShop[] ItemsPlaceList = GetComponentsInChildren<ItemForShop>();

        for(int i = 0; i < ItemsPlaceList.Length; i++)
        {
            if(ItemsPlaceList[i].tag == "ForFixed")
            {
                mFixedItemsPlaceList.Add(ItemsPlaceList[i]);
                ItemsPlaceList[i].MyItemInfo = mFixedItemList[i];
                ItemsPlaceList[i].Renderer.material.SetTexture("_MainTex", mFixedItemList[i].MyIcon.texture);
            }
        }
    }

    private void InitRandomItemPlaces()
    {
        ItemForShop[] ItemsPlaceList = GetComponentsInChildren<ItemForShop>();

        for (int i = 0; i < ItemsPlaceList.Length; i++)
        {
            if (ItemsPlaceList[i].tag == "ForRandom")
            {
                mRandomItemsPlaceList.Add(ItemsPlaceList[i]);
                mRandomItemList.Add(null);
            }
        }
    }

    private void UpdateRandomItemPlace()
    {
        for (int i = 0; i < mRandomItemsPlaceList.Count; i++)
        {
            mRandomItemsPlaceList[i].MyItemInfo = mRandomItemList[i];
            mRandomItemsPlaceList[i].Renderer.material.SetTexture("_MainTex", mRandomItemList[i].MyIcon.texture);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitItemLists();
        InitFixedItemPlaces();
        InitRandomItemPlaces();

        StartCoroutine("UpdateRandomItemList");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
