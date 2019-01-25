using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColorChange : MonoBehaviour
{
    public Text buttonText;
    public Color changeColor;
    private Color mOriginalColor;

    private void Start()
    {
        buttonText = gameObject.GetComponentInChildren<Text>();
        mOriginalColor = buttonText.color;
    }

    public void OnButtonHighlighte()
    {
        buttonText.color = new Color(changeColor.r, changeColor.g, changeColor.b, 255);
    }

    public void OffButtonHighlighte()
    {
        buttonText.color = new Color(mOriginalColor.r, mOriginalColor.g, mOriginalColor.b, 255);
    }

    public void OnClickButton()
    {
        buttonText.color = new Color(mOriginalColor.r, mOriginalColor.g, mOriginalColor.b, 255);
    }
}
