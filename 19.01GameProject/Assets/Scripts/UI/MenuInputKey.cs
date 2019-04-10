using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(MainMenuController))]
public class MenuInputKey : MonoBehaviour
{
    public Color changeTextColor;
    private Color mOriginalTextColor;

    [SerializeField]
    private Button[] mMenuButtons = null;

    private int mbuttonIndex = 0;
    private MainMenuController mpanelControl;
    // Start is called before the first frame update
    
    void Start()
    {
        mOriginalTextColor = mMenuButtons[0].GetComponentInChildren<Text>().color;
        mpanelControl = gameObject.GetComponent<MainMenuController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (mbuttonIndex < mMenuButtons.Length - 1)
            {
                Debug.Log(mbuttonIndex);
                ReturnButtonHighlight(mbuttonIndex);
                mbuttonIndex++;
                NextButtonHighlight(mbuttonIndex);
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (mbuttonIndex > 0)
            {
                Debug.Log(mbuttonIndex);
                ReturnButtonHighlight(mbuttonIndex);
                mbuttonIndex--;
                NextButtonHighlight(mbuttonIndex);
            }
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            EnterButton(mbuttonIndex);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(mpanelControl.optionPanel.activeSelf)
            {
                mpanelControl.optionPanel.SetActive(false);
                mpanelControl.mainPanel.SetActive(true);
            }

            if(mpanelControl.creditPanel.activeSelf)
            {
                mpanelControl.creditPanel.SetActive(false);
                mpanelControl.mainPanel.SetActive(true);
            }
        }
    }

    private void ReturnButtonHighlight(int btnIndex)
    {
        Text btnText = mMenuButtons[btnIndex].GetComponentInChildren<Text>();
        btnText.color = new Color(mOriginalTextColor.r, mOriginalTextColor.g, mOriginalTextColor.b, 255);
    }

    private void NextButtonHighlight(int btnIndex)
    {
        Text btnText = mMenuButtons[btnIndex].GetComponentInChildren<Text>();
        btnText.color = new Color(changeTextColor.r, changeTextColor.g, changeTextColor.b, 255);
    }
    
    private void EnterButton(int btnIndex)
    {
        mMenuButtons[btnIndex].onClick.Invoke();
    }
}
