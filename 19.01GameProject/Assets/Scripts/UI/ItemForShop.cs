using System.Collections;
using System.Collections.Generic;
using Neremnem.Tools;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ItemForShop : MonoBehaviour
{
    private ItemIconVersion mMyItemInfo;        // 내 아이템 정보
    private Renderer mRenderer;

    public bool isFixed;

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

    public Renderer Renderer
    {
        get
        {
            return mRenderer;
        }

        set
        {
            mRenderer = value;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        mRenderer = GetComponent<MeshRenderer>();

        if(isFixed == true)
        {
            gameObject.tag = "ForFixed";
        }
        else
        {
            gameObject.tag = "ForRandom";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            
            if(player.linkedInputManager.AttackButton.State.CurrentState == NRMInput.EButtonStates.Down)
            {
                if(InventoryScript.MyInstance.Gold >= mMyItemInfo.MyCost)
                {
                    InventoryScript.MyInstance.UpdateGold(mMyItemInfo.MyCost);
                }
            }
        }
    }
}
