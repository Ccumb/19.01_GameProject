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
        mMax_hp = GameObject.Find("Player").GetComponent<Player>().max_hp;
    }

    // Update is called once per frame
    void Update()
    {
        mHp = GameObject.Find("Player").GetComponent<Player>().hp;
        string text = "HP : " + mHp + " / " + mMax_hp;
        mText.text = text;
        mHealthBar.fillAmount = mHp / mMax_hp;
    }
}
