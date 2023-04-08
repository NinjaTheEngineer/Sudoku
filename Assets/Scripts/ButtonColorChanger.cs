using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonColorChanger : ColorChanger {
    [SerializeField]
    private Color selectedColor;
    [SerializeField]
    private Color highlightedColor;
    public void SetNormalColor() {
        image.color = normalColor;
    }
    public void SetSelectedColor() {
        image.color = selectedColor;
    }
    public void SetHighlightedColor() {
        image.color = highlightedColor;
    }
}
