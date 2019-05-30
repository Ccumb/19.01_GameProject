using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerMP : MonoBehaviour
{
    private Image mMpBar;

    private Text mText;

    private float mMp;
    private float mMax_mp;

    private void Start()
    {
        mText = GetComponentInChildren<Text>();
        mMpBar = GetComponent<Image>();
        mMax_mp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().MaxSP;

    }

    // Update is called once per frame
    void Update()
    {
        mMp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().SP;
        //string text = "HP : " + mMp + " / " + mMax_mp;
        //mText.text = text;
        mMpBar.fillAmount = mMp / mMax_mp;
    }
}
