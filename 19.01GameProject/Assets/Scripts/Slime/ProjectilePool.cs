using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public int ObjectCount;
    public GameObject ProjectileObject;
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
            GameObject projectile = Instantiate(ProjectileObject);
            projectile.transform.SetParent(transform.parent);
            projectile.layer = 11;
            projectile.SetActive(false);
            PoolList.Add(projectile);
        }
    }

    public void Pooling()
    {
        for (int i = 0; i < PoolList.Count; i++)
        {
            if (PoolList[i].activeSelf == false)
            {
                PoolList[i].SetActive(true);
<<<<<<< HEAD
                PoolList[i].transform.position = gameObject.transform.position;
=======
                PoolList[i].transform.position = new Vector3( gameObject.transform.position.x, gameObject.transform.position.y + 1, gameObject.transform.position.z);
>>>>>>> develop
                return;
            }
        }
    }
}
