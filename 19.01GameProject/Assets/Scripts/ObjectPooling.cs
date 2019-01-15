using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public GameObject poolingObject;
    public int objectNum;

    private List<GameObject> mObjects;

    void Awake()
    {
        for(int i = 0; i< objectNum; i ++)
        {
            mObjects.Add(poolingObject);
            mObjects[i].SetActive(false);
        }
    }
}
