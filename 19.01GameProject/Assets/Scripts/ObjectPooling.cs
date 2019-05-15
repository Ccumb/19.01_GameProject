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
        Time.timeScale = 1;
        mObjects = new List<GameObject>();

        for (int i = 0; i< objectNum; i ++)
        {
            GameObject dropObject = Instantiate(poolingObject);
            dropObject.transform.SetParent(gameObject.transform);
            dropObject.SetActive(false);
            mObjects.Add(dropObject);
        }
    }
}
