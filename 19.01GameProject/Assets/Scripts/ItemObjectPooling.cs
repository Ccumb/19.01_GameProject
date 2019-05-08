﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectPooling : MonoBehaviour
{
    public string[] ItemNames;  //게임 아이템 이름
    public List<ItemIconVersion> ItemObjects    //아이템 정보 사용 리스트(공용)
    {
        get { return mObjects; }
    }
    private List<ItemIconVersion> mObjects = new List<ItemIconVersion>(); //아이템 정보를 넣을 리스트(개인)
    private int mItemCount; //사용할 아이템 개수

    private void Start()
    {
        object[] items = Resources.LoadAll("Items/Random");
        mItemCount = ItemNames.Length;
        for(int i = 0; i < items.Length; i++)
        {
            ItemIconVersion item = items[i] as ItemIconVersion;
            for(int j = 0; j < mItemCount; j++)
            {
                if(item.name == ItemNames[j])
                {
                    //이곳에서 아이템 미리 생성, 정보를 넣음
                    mObjects.Add(item);
                }
            }
        }

        //필요 없는 부분(확인용)
        for (int i = 0; i < mObjects.Count; i++)
        {
            Debug.Log(mObjects[i]);
        }
    }

}
