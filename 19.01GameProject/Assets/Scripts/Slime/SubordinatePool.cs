using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubordinatePool : MonoBehaviour
{
    public int ObjectCount;
    public GameObject SubordinateObject;
    private List<GameObject> PoolList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        ObjectPool();
    }

    public void ObjectPool()
    {
        for (int i = 0; i < ObjectCount; i++)
        {
            GameObject subordinate = Instantiate(SubordinateObject);
            subordinate.transform.SetParent(transform.parent.GetChild(1));
            //subordinate.layer = 11;
            subordinate.SetActive(false);
            PoolList.Add(subordinate);
        }
    }

    public void Pooling(int poolCount, Transform[] poolPosition)
    {
        for (int i = 0; i < poolCount; i++)
        {
            if (PoolList[i].activeSelf == false)
            {
                PoolList[i].SetActive(true);
                PoolList[i].transform.position = poolPosition[i].position;
            }
        }
    }
}
