﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] public GameObject mainPanel;
    [SerializeField] public GameObject optionPanel;
    [SerializeField] public GameObject creditPanel;
    [SerializeField] public GameObject keyinfoPanel;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void ShowMainPanel()
    {
        mainPanel.SetActive(true);
    }
    public void HideMainPanel()
    {
        mainPanel.SetActive(false);
    }
    public void ShowOptionPanel()
    {
        optionPanel.SetActive(true);
        HideMainPanel();
    }
    public void HideOptionPanel()
    {
        optionPanel.SetActive(false);
        ShowMainPanel();
    }
    public void ShowCreditPanel()
    {
        creditPanel.SetActive(true);
        HideMainPanel();
    }
    public void HideCreditPanel()
    {
        creditPanel.SetActive(false);
        ShowMainPanel();
    }
    public void ShowKeyInfoPanel()
    {
        keyinfoPanel.SetActive(true);
        HideMainPanel();
    }
    public void HideKeyInfoPanel()
    {
        keyinfoPanel.SetActive(false);
        ShowMainPanel();
    }


    public void EnterExitButton()
    {
        Application.Quit();
    }

    public void ClickStartButton()
    {
        //Time.timeScale = 1;
        SceneManager.LoadScene("Town");        
    }
}
