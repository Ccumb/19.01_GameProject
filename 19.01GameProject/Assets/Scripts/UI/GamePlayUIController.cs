using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    public GameObject inventoryPanel;
    public GameObject skillPanel;
    public GameObject GamePlayQuickSlot;

    private List<ActionButton> mGamePlayQuickSlots = new List<ActionButton>();

    private KeyCode mSkill1, mSkill2, mSkill3, mSkill4;

    [SerializeField]
    private Button[] mSkillButtons;

    private static UIManager mInstance;

    public static UIManager MyInstance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = FindObjectOfType<UIManager>();
            }
            return mInstance;
        }

        set
        {
            mInstance = value;
        }
    }

    public void UpdateStackSize(IClickable clickable)
    {
        if (clickable.MyCount > 1)
        {
            // 해당 슬롯의 중첩개수 표시하기
            clickable.MyStackText.text = clickable.MyCount.ToString();
            clickable.MyStackText.color = Color.white;
            clickable.MyIcon.color = Color.white;
        }

        else
        {
            // 해당 슬롯의 텍스트 투명하게 만들기
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
        }

        if (clickable.MyCount == 0)
        {
            // 해당 슬롯의 아이콘 투명하게 만들기
            clickable.MyIcon.color = new Color(0, 0, 0, 0);
            // 해당 슬롯의 텍스트 투명하게 만들기
            clickable.MyStackText.color = new Color(0, 0, 0, 0);

        }
    }
    void Start()
    {
        PlayerSkill player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkill>();
        mSkill1 = player.skill1;
        mSkill2 = player.skill2;
        mSkill3 = player.skill3;
        mSkill4 = player.skill4;

        for (int i = 0; i < GamePlayQuickSlot.transform.childCount; i++)
        {
            mGamePlayQuickSlots.Add(GamePlayQuickSlot.transform.GetChild(i).GetComponent<ActionButton>());
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(inventoryPanel.activeSelf)
            {
                Time.timeScale = 1;
                DeActivePanel(inventoryPanel);
                HandScript.MyInstance.Drop();
                SyncItemQuickSlots();
            }

            else if(!inventoryPanel.activeSelf)
            {
                Time.timeScale = 0;
                ActivePanel(inventoryPanel);
                //InventoryScript.MyInstance.SetInveontryQuickSlotList(mGamePlayQuickSlots);
            }            
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            if(inventoryPanel.activeSelf)
            {
                DeActivePanel(inventoryPanel);
            }
            if(!skillPanel.activeSelf)
            {
                Time.timeScale = 0;
                ActivePanel(skillPanel);
            }
            else if(skillPanel.activeSelf)
            {
                Time.timeScale = 1;
                DeActivePanel(skillPanel);
                HandScript.MyInstance.Drop();
            }           
        }
        if(Input.GetKeyDown(mSkill1))
        {
            ActionButtonOnClick(0);
        }

        if(Input.GetKeyDown(mSkill2))
        {
            ActionButtonOnClick(1);
        }

        if(Input.GetKeyDown(mSkill3))
        {
            ActionButtonOnClick(2);
        }

        if(Input.GetKeyDown(mSkill4))
        {
            ActionButtonOnClick(3);
        }

    }

    public void ActivePanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void DeActivePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    //이름이 바뀌었을 때랑 카운트가 0인것을 확인을 하고 동기화를 시켜줘야할지
    //아니면 그냥 계속 for문 돌면서 확인하게 할지도 고민을 좀 해야봐야할 것 같다.
    private void SyncItemQuickSlots()
    {
        List<ActionButton> action = InventoryScript.MyInstance.GetInventoryQuickSlotList();
        for(int i = 0; i < action.Count; i++)
        {
            Sprite sprite = action[i].MyIcon.sprite;
            mGamePlayQuickSlots[i].MyUseable = action[i].MyUseable;
            mGamePlayQuickSlots[i].MyIcon.sprite = sprite;
            mGamePlayQuickSlots[i].objectname = action[i].objectname;
        }
    }

    private void ActionButtonOnClick(int btnIndex)
    {
        mSkillButtons[btnIndex].onClick.Invoke();
    }

}
