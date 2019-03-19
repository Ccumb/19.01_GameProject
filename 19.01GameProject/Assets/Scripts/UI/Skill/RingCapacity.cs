using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingCapacity : MonoBehaviour
{
    [SerializeField]
    private GameObject mCapacityPrefab;

    public int ringcount = 6;
    // Start is called before the first frame update

    private List<GameObject> mCapacity = new List<GameObject>();
    
    public List<GameObject> MyCapacity
    {
        get
        {
            return mCapacity;
        }
    }
    
    public void AddCapacity(int count)
    {
        for(int i = 0; i < count; i ++)
        {
            GameObject ring = Instantiate(mCapacityPrefab, transform);
            mCapacity.Add(ring);
        }
    }

    void Start()
    {
        AddCapacity(ringcount);
    }  
}
