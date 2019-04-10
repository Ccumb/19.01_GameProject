using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerHP : MonoBehaviour
{
    private Image mHealthBar;
    
    private Text mText;

    private float mHp;
    private float mMax_hp;

    private void Start()
    {
        mText = GetComponentInChildren<Text>();
        mHealthBar = GetComponent<Image>();
        mMax_hp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().MaxHP;
        
    }

    // Update is called once per frame
    void Update()
    {
        mHp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().HP;
        string text = "HP : " + mHp + " / " + mMax_hp;
        mText.text = text;
        mHealthBar.fillAmount = mHp / mMax_hp;
    }
}
