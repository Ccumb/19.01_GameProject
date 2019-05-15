using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAdd : MonoBehaviour
{
    private ItemIconVersion mMyItemInfo;

    public ItemIconVersion MyItemInfo
    {
        get
        {
            return mMyItemInfo;
        }

        set
        {
            mMyItemInfo = value;
        }
    }

    void Start()
    {
        if(mMyItemInfo == null)
        {
            Debug.Log("Item information error");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HealthPotion potion = (HealthPotion)Instantiate(mMyItemInfo);
            InventoryScript.MyInstance.AddItem(potion);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            HealthPotion potion = (HealthPotion)Instantiate(mMyItemInfo);
            Debug.Log("mMyItemInfo: " + mMyItemInfo);
            Debug.Log("potion: " + potion);
            InventoryScript.MyInstance.AddItem(potion);
            gameObject.SetActive(false);
        }
    }
}
