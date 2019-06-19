﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerGold : MonoBehaviour
{
    private Text mText;

    private void Start()
    {
        mText = GetComponent<Text>();
    }

    private void Update()
    {
        mText.text = "Gold : " + InventoryScript.MyInstance.Gold.ToString();
    }
}
