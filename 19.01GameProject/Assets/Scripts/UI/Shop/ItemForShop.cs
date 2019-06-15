using System.Collections;
using System.Collections.Generic;
using Neremnem.Tools;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ItemForShop : MonoBehaviour
{
    private ShopSystem shop;
    private ItemIconVersion mMyItemInfo;        // 내 아이템 정보
    private Renderer mRenderer;
    public bool mIsFull;                      // 구매할 수 있는가?

    public bool isFixed;                        // 고정용 아이템용 칸인가

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

    IEnumerator CheckItemForSell()      // 판매 가능한지 Check
    {
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            mIsFull = (InventoryScript.MyInstance.ItemIsFull(mMyItemInfo.name));  // 슬롯에 해당 아이템이 꽉 차있는 경우 -> 판매 불가
            if (mIsFull == true)      
            {
                mRenderer.material.SetFloat("_isFull", -0.7f);      // 판매 불가용 텍스쳐로 변경
            }
            else
            {
                mRenderer.material.SetFloat("_isFull", 0.7f);
            }

            yield return new WaitForSeconds(0.3f);              // 매 프레임 체크하는 것은 비효율적인 것으로 판단, 일정 주기마다 판단.
        }
    }
    
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

        mIsFull = false;
    }

    void Start()
    {
        shop = GetComponentInParent<ShopSystem>();
        StartCoroutine("CheckItemForSell");
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


            if (player.linkedInputManager.AttackButton.State.CurrentState == NRMInput.EButtonStates.Down 
                || player.linkedInputManager.AttackButton.State.CurrentState == NRMInput.EButtonStates.Pressed)
            {
                //Debug.Log(InventoryScript.MyInstance.ItemIsFull(mMyItemInfo.name));

                if(InventoryScript.MyInstance.Gold >= mMyItemInfo.MyCost/* && mIsFull == false*/)
                {
                    InventoryScript.MyInstance.UpdateGold(-mMyItemInfo.MyCost);
                    shop.PlayBuySuccessful();
                    ItemIconVersion potion = (HealthPotion)Instantiate(mMyItemInfo);
                    InventoryScript.MyInstance.AddItem(potion);
                }
                else
                {
                    shop.PlayBuyFail();
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
