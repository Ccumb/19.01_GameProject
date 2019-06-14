using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotChange : MonoBehaviour
{
    public GameObject actionPrefab;
    [SerializeField]
    private GameObject mSelectImage;

    [SerializeField]
    private GameObject mUnSelectImage;

    private ActionButton tempAction;
    private ActionButton mUnSelectAction;
    private ActionButton mSelectAction;

    public void UseItem()
    {
        mSelectAction.OnClick();
    }

    void Start()
    {
        tempAction = Instantiate(actionPrefab).GetComponent<ActionButton>();
        mUnSelectAction = mUnSelectImage.GetComponent<ActionButton>();
        mSelectAction = mSelectImage.GetComponent<ActionButton>();
    }

    public void SwapButton()
    {
        tempAction = Swap(tempAction, mSelectAction);
        Swap(mSelectAction, mUnSelectAction);
        Swap(mUnSelectAction, tempAction);
    }
    private ActionButton Swap(ActionButton swap1, ActionButton swap2)
    {
        if(swap2.objectname != "Empty" && swap2.objectname != "")
        {
            swap1.MyUseables = swap2.MyUseables;
            swap1.MyCount = swap2.MyUseables.Count;
            swap1.MyIcon.sprite = swap2.MyIcon.sprite;
            swap1.MyIcon.color = Color.white;
            swap1.MyStackText.text = swap1.MyCount.ToString();
            swap1.objectname = swap2.objectname;
        }
        else
        {
            swap1.MyUseables = swap2.MyUseables;
            swap1.MyCount = swap2.MyUseables.Count;
            swap1.MyIcon.sprite = swap2.MyIcon.sprite;
            swap1.MyIcon.color = new Color(0, 0, 0, 0);
            swap1.MyStackText.text = "";
            swap1.objectname = "Empty";
        }
        return swap1;
    }
}
