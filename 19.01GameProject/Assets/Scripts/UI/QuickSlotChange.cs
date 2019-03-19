using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotChange : MonoBehaviour
{
    [SerializeField]
    private ActionButton mSelectImage;

    [SerializeField]
    private ActionButton mUnSelectImage;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("Test");
            //2번째 퀵슬롯에 아무것도 안들어 있을때도 교체가 가능해야 한다 이러면 if문의 조건을 바꾸고 애니메이션을 추가해야 할 것 같다
            if (mUnSelectImage != null)
            {
                SwapButton();
            }
        }
    }

    private void SwapButton()
    {
        ActionButton action = mSelectImage;
        mSelectImage = mUnSelectImage;
        mUnSelectImage = action;
    }
}
