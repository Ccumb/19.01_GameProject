using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] public GameObject mainPanel;
    [SerializeField] public GameObject optionPanel;
    [SerializeField] public GameObject creditPanel;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
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
    public void EnterExitButton()
    {
        Application.Quit();
    }

    public void ClickStartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
