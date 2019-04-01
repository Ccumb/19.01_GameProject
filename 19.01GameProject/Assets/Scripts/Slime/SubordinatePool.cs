using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SubordinatePool : MonoBehaviour
{
    public int ObjectCount; //몇 개를 풀링할 것인지
    public GameObject SubordinateObject; //오브젝트 풀링할 객체
    private List<GameObject> mPoolList = new List<GameObject>(); // 풀링할 객체를 넣을 리스트

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
            mPoolList.Add(subordinate);
        }
    }

    public void Pooling(int poolCount, Transform[] poolPosition)
    {
        for (int i = 0; i < poolCount; i++)
        {
            if (mPoolList[i].activeSelf == false)
            {
                mPoolList[i].SetActive(true);
                mPoolList[i].transform.position = poolPosition[i].position;
            }
        }
    }
}
