using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RingCapacity : MonoBehaviour
{
    public Sprite full_ring;
    public Sprite empty_ring;
    [SerializeField]
    private GameObject mCapacityPrefab;

    public int currentRingcount = 0;
    public static int maxRingcount = 6;
    // Start is called before the first frame update

    private List<GameObject> mCapacity = new List<GameObject>();
    private Sprite[] mRingCapacitys = new Sprite[maxRingcount];

    private static RingCapacity mInstance;

    public static RingCapacity MyInstance
    {
        get
        {
            if(mInstance == null)
            {
                mInstance = FindObjectOfType<RingCapacity>();
            }
            return mInstance;
        }
        set
        {
            mInstance = value;
        }
    }

    public List<GameObject> MyCapacity
    {
        get
        {
            return mCapacity;
        }
    }
    public int MyCurrentRingCount
    {
        set { currentRingcount = value; }
    }
    
    public void AddCapacity(int count)
    {
        for(int i = 0; i < count; i ++)
        {
            GameObject ring = Instantiate(mCapacityPrefab, transform);
            Sprite sprite = ring.GetComponent<Image>().sprite;
            mCapacity.Add(ring);
            mRingCapacitys[i] = sprite;
        }
    }

    public bool IsCheckRegistSkill(int count)
    {
        if(count + currentRingcount < maxRingcount)
        {
            return true;
        }
        else
            return false;
    }

    public void UpdateRingVisual(int count)
    {
        if(count > currentRingcount)
        {
            for(int i = 0; i < count; i++)
            {
                MyCapacity[i].GetComponent<Image>().sprite = full_ring;
            }          
        }
        else if(count < currentRingcount)
        {
            Debug.Log("Count < CurrentRingCount");
            for(int i = count; i < currentRingcount; i++)
            {
                MyCapacity[i].GetComponent<Image>().sprite = empty_ring;
            }           
        }
        currentRingcount = count;
    }

    void Start()
    {
        AddCapacity(maxRingcount);
        SkillSettingScript.MyInstance.RingCountchange += new RingCountChanged(UpdateRingVisual);
    }  
}
