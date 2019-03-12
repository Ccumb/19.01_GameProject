using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;
public class ObjectPooling : MonoBehaviour
{
    public GameObject poolingObject;
    public GameObject normalSlime;
    public GameObject rushSlime;
    public GameObject[] poolingRangeObject;
    private GameObject[] mNormalSlimePool;
    private GameObject[] mRushSlimePool;
    public int objectNum;
    private List<GameObject> mObjects;
    private List<GameObject> mRangeObjects;

    public List<GameObject> obejcts
    {
        get
        {
            return mObjects;
        }
    }


    private void Awake()
    {
        Time.timeScale = 1;
        mObjects = new List<GameObject>();
        mRangeObjects = new List<GameObject>();

        for (int i = 0; i< objectNum; i ++)
        {
            GameObject gameObject = Instantiate(poolingObject);
            gameObject.SetActive(false);
            mObjects.Add(gameObject);
        }
        for(int i = 0; i< 5; i++)
        {
            GameObject temp1 = Instantiate(normalSlime);
            GameObject temp2 = Instantiate(rushSlime);
            temp1.SetActive(false);
            temp2.SetActive(false);
            mNormalSlimePool[i] = temp1;
            mRushSlimePool[i] = temp2;
        }
    }
    private void OnEnable()
    {
        EventManager.StartListeningCommonEvent("SpawnNormalSlime", SpawnNormalSlime);
        EventManager.StartListeningCommonEvent("SpawnRushSlime", SpawnRushSlime);       
    }
    private void OnDisable()
    {
        EventManager.StopListeningCommonEvent("SpawnNormalSlime", SpawnNormalSlime);
        EventManager.StopListeningCommonEvent("SpawnRushSlime", SpawnRushSlime);      
    }
    private void PoolTemporarily(int i, Vector3 poisition)
    {
        switch (i)
        {
            case 0:
                mRangeObjects[0].transform.position = poisition;
                mRangeObjects[0].SetActive(true);
                break;
            case 1:
                mRangeObjects[1].transform.position = poisition;
                mRangeObjects[1].SetActive(true);
                break;
            case 2:
                mRangeObjects[2].transform.position = poisition;
                mRangeObjects[2].SetActive(true);
                break;
        }

    }
    private void SpawnNormalSlime()
    {
        for(int i = 0; i< mNormalSlimePool.Length; i++)
        {
            if(mNormalSlimePool[i].active == false)
            {
                mNormalSlimePool[i].SetActive(true);
                break;
            }
        }
    }
    private void SpawnRushSlime()
    {
        for (int i = 0; i < mRushSlimePool.Length; i++)
        {
            if (mRushSlimePool[i].active == false)
            {
                mRushSlimePool[i].SetActive(true);
                break;
            }
        }
    }
}
