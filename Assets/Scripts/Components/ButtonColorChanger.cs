using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonColorChanger : ColorChanger {
    [SerializeField]
    private Color _selectedColor;
    public Color SelectedColor => _selectedColor;
    [SerializeField]
    private Color _highlightedColor;
    public Color HighlightedColor => _highlightedColor;
    public void SetNormalColor() {
        SetColor(NormalColor);
    }
    public void SetSelectedColor() {
        SetColor(SelectedColor);
    }
    public void SetHighlightedColor() {
        SetColor(HighlightedColor);
    }
}
