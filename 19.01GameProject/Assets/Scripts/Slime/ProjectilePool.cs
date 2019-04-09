using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public int ObjectCount;                                         //몇 개를 풀링할 것인지
    public GameObject ProjectileObject;                             //오브젝트 풀링할 객체
    private List<GameObject> mPoolList = new List<GameObject>();    //풀링할 객체를 넣을 리스트

    void Start()
    {
        ObjectPool();
    }
    /// <summary>
    /// 오브젝트 풀링
    /// </summary>
    public void ObjectPool()
    {
        for (int i = 0; i < ObjectCount; i++)
        {
            GameObject projectile = Instantiate(ProjectileObject);
            projectile.transform.SetParent(transform.parent);
            projectile.layer = 11;
            projectile.SetActive(false);
            mPoolList.Add(projectile);
        }
    }
    /// <summary>
    /// 풀링을 한 개씩만 하는 것, 여러 개 발사는 불가능
    /// </summary>
    public void Pooling()
    {
        for (int i = 0; i < mPoolList.Count; i++)
        {
            if (mPoolList[i].activeSelf == false)
            {
                mPoolList[i].SetActive(true);
                mPoolList[i].transform.position = new Vector3( gameObject.transform.position.x, gameObject.transform.position.y + 1, gameObject.transform.position.z);
                return;
            }
        }
    }
}
