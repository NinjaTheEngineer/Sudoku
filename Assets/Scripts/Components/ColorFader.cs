using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorFader : NinjaMonoBehaviour {
    public float defaultFadeTime = 1f;
    [SerializeField]
    private Image image;
    private void Awake() {
        string logId = "Awake";
        if (image == null) {
            loge(logId, "Image component not found on this GameObject!");
            image = GetComponent<Image>();
        }
    }
    public void FadeIn(float fadeTime=-1) {
        string logId = "FadeIn";
        if(image==null) {
            logw(logId, "Image="+image.logf()+" => no-op");
            return;
        }
        Color startColor = image.color;
        Color endColor = startColor;
        startColor.a = 0;
        endColor.a = 1;
        StartCoroutine(FadeRoutine(startColor, endColor, fadeTime));
    }
    public void FadeOut(float fadeTime=-1) {
        string logId = "FadeOut";
        if(image==null) {
            logw(logId, "Image="+image.logf()+" => no-op");
            return;
        }
        Color startColor = image.color;
        Color endColor = startColor;
        startColor.a = 1;
        endColor.a = 0;
        StartCoroutine(FadeRoutine(startColor, endColor, fadeTime));
    }
    public void FadeColors(Color startColor, Color endColor, float fadeTime=-1) {
        string logId = "FadeColors";
        if(image==null) {
            logw(logId, "Image="+image.logf()+" => no-op");
            return;
        }
        StartCoroutine(FadeRoutine(startColor, endColor, fadeTime));
    }
    IEnumerator FadeRoutine(Color startColor, Color endColor, float fadeTime=-1) {
        string logId = "FadeRoutine";
        logt(logId, "Starting fading from "+startColor+" to "+endColor);
        fadeTime = fadeTime==-1?defaultFadeTime:fadeTime;
        while (true) {
            float t = 0f;
            while (t < fadeTime) {
                t += Time.deltaTime;
                image.color = Color.Lerp(startColor, endColor, t / fadeTime);
                yield return null;
            }
            break;
        }
    }
}