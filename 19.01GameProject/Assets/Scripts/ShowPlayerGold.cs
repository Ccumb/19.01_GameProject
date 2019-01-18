using System.Collections;
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

    // Update is called once per frame
    void Update()
    {
        string text = "Gold : " + GameObject.Find("Player").GetComponent<Player>().mGold;
        mText.text = text;
    }
}
