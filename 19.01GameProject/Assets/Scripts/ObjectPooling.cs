using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public GameObject poolingObject;
    public int objectNum;
    private List<GameObject> mObjects;

    public List<GameObject> obejcts
    {
        get
        {
            return mObjects;
        }
    }

    void Awake()
    {
        mObjects = new List<GameObject>();

        for (int i = 0; i< objectNum; i ++)
        {
            GameObject gameObject = Instantiate(poolingObject);
            gameObject.SetActive(false);
            mObjects.Add(gameObject);
        }
    }
}
