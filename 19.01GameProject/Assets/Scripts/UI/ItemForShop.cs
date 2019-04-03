using System.Collections;
using System.Collections.Generic;
using Neremnem.Tools;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ItemForShop : MonoBehaviour
{
    private ItemIconVersion mMyItemInfo;        // 내 아이템 정보
    private Renderer mRenderer;
    private bool mCanSell;

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

    IEnumerator CheckItemForSell()
    {
        while(true)
        {
            mCanSell = !(InventoryScript.MyInstance.ItemIsFull(mMyItemInfo.name));

            if (mCanSell == false)
            {
                mRenderer.material.SetFloat("_isFull", 1);
            }
            else
            {
                mRenderer.material.SetFloat("_isFull", 0);
            }

            yield return new WaitForSeconds(1.5f);
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

        mCanSell = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            PlayerAttack attack = other.GetComponent<PlayerAttack>();
            attack.enabled = false;

            Debug.Log("Inside");

            if (player.linkedInputManager.AttackButton.State.CurrentState == NRMInput.EButtonStates.Down 
                || player.linkedInputManager.AttackButton.State.CurrentState == NRMInput.EButtonStates.Pressed)
            {
                Debug.Log("Buy Something");
                
                if(InventoryScript.MyInstance.Gold >= mMyItemInfo.MyCost && mCanSell == true)
                {
                    InventoryScript.MyInstance.UpdateGold(-mMyItemInfo.MyCost);

                    HealthPotion potion = (HealthPotion)Instantiate(mMyItemInfo);
                    InventoryScript.MyInstance.AddItem(potion);
                    
                    //Debug.Log("current Gold : " + InventoryScript.MyInstance.Gold);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            PlayerAttack attack = other.GetComponent<PlayerAttack>();
            attack.enabled = true;
        }
    }
}
