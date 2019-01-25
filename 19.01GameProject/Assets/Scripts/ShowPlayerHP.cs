using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerHP : MonoBehaviour
{
    Image healthBar;
    
    private Text mText;

    private float hp;
    private float max_hp;

    private void Start()
    {
        mText = GetComponentInChildren<Text>();
        healthBar = GetComponent<Image>();
        max_hp = GameObject.Find("Player").GetComponent<Player>().max_hp;
    }

    // Update is called once per frame
    void Update()
    {
        hp = GameObject.Find("Player").GetComponent<Player>().hp;
        string text = "HP : " + hp + " / " + max_hp;
        mText.text = text;
        healthBar.fillAmount = hp / max_hp;
    }
}
