using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcquireHealthPotion : MonoBehaviour
{
    public ItemIconVersion itemData;
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            InventoryScript.MyInstance.AddItem(itemData);
            Destroy(this.gameObject);
        }
    }
}
