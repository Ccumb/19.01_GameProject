using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColorChange : MonoBehaviour
{
    public Text text;
    public Color changeColor;
    private Color originalColor;

    private void Start()
    {
        text = gameObject.GetComponentInChildren<Text>();
        originalColor = text.color;
    }

    public void OnButtonHighlighte()
    {
        text.color = new Color(changeColor.r, changeColor.g, changeColor.b, 255);
    }

    public void OffButtonHighlighte()
    {
        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 255);
    }

    public void OnClickButton()
    {
        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 255);
    }
}
