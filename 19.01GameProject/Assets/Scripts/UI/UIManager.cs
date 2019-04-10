using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//PlayerSkill에서 Skill[0]을 넘겨줘서 여기 있는 함수에서 ActionButton들과 이름이 동일한 것의
//스킬의 쿨타임을 갱신해주는것
public delegate void CoolTimeCountChanged(Skill skill);
public delegate void ItemCountChanged(ItemIconVersion item);
public delegate void UpdatePlayerSkillList(List<ActionButton> actions);
public class UIManager : MonoBehaviour
{
    public bool chagneAvailable = true;
    public event ItemCountChanged itemCountChangedEvent;
    public event UpdatePlayerSkillList Regist;

    [SerializeField]
    public GameObject inventoryPanel;
    public GameObject skillPanel;
    public GameObject GamePlayQuickSlot;
    public GameObject GamePlaySkillQuickSlot;

    private List<ActionButton> mGamePlayQuickSlots = new List<ActionButton>();
    private List<ActionButton> mGamePlaySkillQuickSlots = new List<ActionButton>();

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

    public void OnItemCountChanged(ItemIconVersion item)
    {
        if(itemCountChangedEvent != null)
        {
            itemCountChangedEvent.Invoke(item);
        }
    }

    public void UpdateCoolTimeText(Skill skill)
    {
        for(int i = 0; i < mGamePlaySkillQuickSlots.Count; i++)
        {
            if(skill.name == mGamePlaySkillQuickSlots[i].objectname)
            {
                if(skill.MyCoolTime == 0)
                {
                    mGamePlaySkillQuickSlots[i].MyCoolTimeText.text = "";
                    return;
                }
                mGamePlaySkillQuickSlots[i].MyCoolTimeText.text = skill.MyCoolTime.ToString();
                return;
                //여기서 쿨타임 업데이트허기
            }
            Debug.Log("Not match objectname");
        }
    }
    public void UpdateStackSize(IClickable clickable)
    {
        if (clickable.MyCount > 1)
        {
            clickable.MyStackText.text = clickable.MyCount.ToString();
            // 해당 슬롯의 중첩개수 표시하기
            clickable.MyStackText.color = Color.white;
            clickable.MyIcon.color = Color.white;
        }
        else if(clickable.MyCount == 0)
        {

            // 해당 슬롯의 아이콘 투명하게 만들기
            clickable.MyIcon.color = new Color(0, 0, 0, 0);
            // 해당 슬롯의 텍스트 투명하게 만들기
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
        }
        else
        {
            clickable.MyStackText.text = clickable.MyCount.ToString();
            // 해당 슬롯의 텍스트 투명하게 만들기
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
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
        List<ActionButton> action = InventoryScript.MyInstance.GetInventoryQuickSlotList();//이거 프로퍼티로 바꾸기 19.04.01
        for(int i = 0; i < action.Count; i++)
        {
            if(action[i].MyUseables != null && action[i].MyCount > 0)
            {               
                mGamePlayQuickSlots[i].MyUseables = action[i].MyUseables;
                mGamePlayQuickSlots[i].MyStackText.text = action[i].MyStackText.text;
                mGamePlayQuickSlots[i].MyIcon.color = Color.white;
            }
            else
            {
                mGamePlayQuickSlots[i].MyIcon.color = new Color(0, 0, 0, 0);
                mGamePlayQuickSlots[i].MyStackText.text = "";
            }
            Sprite sprite = action[i].MyIcon.sprite;
            mGamePlayQuickSlots[i].MyIcon.sprite = sprite;
            mGamePlayQuickSlots[i].objectname = action[i].objectname;
        }
    }
    private void SyncSkillQuickSlots()
    {
        List<ActionButton> action = SkillSettingScript.MyInstance.MySkillList;
        for(int i = 0; i < action.Count; i++)
        {            
            if(action[i].MyUseable != null)
            {
                mGamePlaySkillQuickSlots[i].MyUseable = action[i].MyUseable;
                mGamePlaySkillQuickSlots[i].MyIcon.color = Color.white;
            }
            else
            {
                mGamePlaySkillQuickSlots[i].MyIcon.color = new Color(0,0,0,0);               
            }
            Sprite sprite = action[i].MyIcon.sprite;
            mGamePlaySkillQuickSlots[i].MyIcon.sprite = sprite;
            mGamePlaySkillQuickSlots[i].objectname = action[i].objectname;
            mGamePlaySkillQuickSlots[i].MyCoolTimeText.text = "";
        }
    }
    void Start()
    {
        PlayerSkill player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkill>();

        for(int i = 0; i < GamePlayQuickSlot.transform.childCount; i++)
        {
            mGamePlayQuickSlots.Add(GamePlayQuickSlot.transform.GetChild(i).GetComponent<ActionButton>());
        }
        for(int i = 0; i < GamePlaySkillQuickSlot.transform.childCount; i++)
        {
            mGamePlaySkillQuickSlots.Add(GamePlaySkillQuickSlot.transform.GetChild(i).GetComponent<ActionButton>());
        }


    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(skillPanel.activeSelf)
            {
                DeActivePanel(skillPanel);
            }
            if(inventoryPanel.activeSelf)
            {
                Time.timeScale = 1;
                SyncItemQuickSlots();
                DeActivePanel(inventoryPanel);
                chagneAvailable = false;
                HandScript.MyInstance.Drop();
            }

            else if(!inventoryPanel.activeSelf)
            {
                Time.timeScale = 0;
                chagneAvailable = true;
                ActivePanel(inventoryPanel);
                //InventoryScript.MyInstance.SetInveontryQuickSlotList(mGamePlayQuickSlots);
            }
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            if(skillPanel != null)
            {
                if(inventoryPanel.activeSelf)
                {
                    DeActivePanel(inventoryPanel);
                }
                if(!skillPanel.activeSelf)
                {
                    Time.timeScale = 0;
                    ActivePanel(skillPanel);
                    chagneAvailable = true;
                }
                else if(skillPanel.activeSelf)
                {
                    Time.timeScale = 1;
                    chagneAvailable = false;
                    SyncSkillQuickSlots();
                    Regist.Invoke(SkillSettingScript.MyInstance.MySkillList);
                    DeActivePanel(skillPanel);
                    HandScript.MyInstance.Drop();
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(skillPanel.activeSelf)
            {
                DeActivePanel(skillPanel);
                ActivePanel(inventoryPanel);
            }
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            if(inventoryPanel.activeSelf)
            {
                DeActivePanel(inventoryPanel);
                ActivePanel(skillPanel);
            }
        }
    }
}
