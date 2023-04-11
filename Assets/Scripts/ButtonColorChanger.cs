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
        image.color = _normalColor;
    }
    public void SetSelectedColor() {
        image.color = _selectedColor;
    }
    public void SetHighlightedColor() {
        image.color = _highlightedColor;
    }
}
