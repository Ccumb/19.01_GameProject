using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUIController : MonoBehaviour
{
    [SerializeField] public GameObject inventoryPanel;
    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.I))
        {
            if(inventoryPanel.activeSelf)
            {
                Time.timeScale = 1;
                inventoryPanel.SetActive(false);
            }

            else if(!inventoryPanel.activeSelf)
            {
                Time.timeScale = 0;
                inventoryPanel.SetActive(true);
            }            
        }
    }
}
