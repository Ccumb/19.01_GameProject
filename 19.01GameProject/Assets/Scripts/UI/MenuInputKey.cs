using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(MainMenuController))]
public class MenuInputKey : MonoBehaviour
{
    public Color changeTextColor;
    private Color originalTextColor;

    [SerializeField]
    private Button[] menuButtons;

    private int buttonIndex = 0;
    private MainMenuController panelControl;
    // Start is called before the first frame update
    
    void Start()
    {
        originalTextColor = menuButtons[0].GetComponentInChildren<Text>().color;
        panelControl = gameObject.GetComponent<MainMenuController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (buttonIndex < menuButtons.Length - 1)
            {
                Debug.Log(buttonIndex);
                ReturnButtonHighlight(buttonIndex);
                buttonIndex++;
                NextButtonHighlight(buttonIndex);
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (buttonIndex > 0)
            {
                Debug.Log(buttonIndex);
                ReturnButtonHighlight(buttonIndex);
                buttonIndex--;
                NextButtonHighlight(buttonIndex);
            }
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            EnterButton(buttonIndex);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(panelControl.optionPanel.activeSelf)
            {
                panelControl.optionPanel.SetActive(false);
                panelControl.mainPanel.SetActive(true);
            }

            if(panelControl.creditPanel.activeSelf)
            {
                panelControl.creditPanel.SetActive(false);
                panelControl.mainPanel.SetActive(true);
            }
        }
    }

    private void ReturnButtonHighlight(int btnIndex)
    {
        Text btnText = menuButtons[btnIndex].GetComponentInChildren<Text>();
        btnText.color = new Color(originalTextColor.r, originalTextColor.g, originalTextColor.b, 255);
    }

    private void NextButtonHighlight(int btnIndex)
    {
        Text btnText = menuButtons[btnIndex].GetComponentInChildren<Text>();
        btnText.color = new Color(changeTextColor.r, changeTextColor.g, changeTextColor.b, 255);
    }
    
    private void EnterButton(int btnIndex)
    {
        menuButtons[btnIndex].onClick.Invoke();
    }
}
