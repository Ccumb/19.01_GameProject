using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class PlayerUseItem : PlayerAbility
{   
    protected string mSelectedItem = "메인아이템";
    protected string mSubItem = "서브";
    private QuickSlotChange useItem;

    void Start()
    {
        base.Start();
        useItem = GameObject.Find("QuickSlots").GetComponent<QuickSlotChange>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mInputManager.UseItemButton.State.CurrentState
            == Neremnem.Tools.NRMInput.EButtonStates.Down
            //|| mInputManager.UseItemButton.State.CurrentState
            //== Neremnem.Tools.NRMInput.EButtonStates.Pressed
            )
        {
            //Debug.Log("아이템 사용");
            Debug.Log(mSelectedItem);
            useItem.UseItem();
            //이벤트 날리기
            //아이템 쿨 존재하면 기다리기
        }
        if(mInputManager.SwapItemButton.State.CurrentState
            == Neremnem.Tools.NRMInput.EButtonStates.Down)
        {
            useItem.SwapButton();
            SwapItem();
        }
    }
    private void SwapItem()
    {
        string temp = mSubItem;
        mSubItem = mSelectedItem;
        mSelectedItem = temp;
    }
}
