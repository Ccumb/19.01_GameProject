using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public GameObject poolingObject;
    public GameObject[] poolingRangeObject;
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


    void Awake()
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
    }

    void Start()
    {

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
}
