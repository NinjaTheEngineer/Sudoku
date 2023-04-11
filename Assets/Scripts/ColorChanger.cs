using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ColorChanger : NinjaMonoBehaviour {
    protected Color _normalColor;
    public Color NormalColor => _normalColor;
    [SerializeField]
    protected Image image;
    private void Awake() {
        string logId = "Awake";
        if (image == null) {
            loge(logId, "Image component not found on this GameObject!");
            image = GetComponent<Image>();
        }
        _normalColor = image.color;
    }
}
