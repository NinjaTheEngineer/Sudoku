using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : NinjaMonoBehaviour {
    public float fadeTime = 1.0f;
    public Image fadeImage;
    public static SceneTransition Instance { get; private set;}
    private void Awake() {
        Instance = this;
    }

    private void Start() {
        StartCoroutine(FadeInRoutine());
    }

    private IEnumerator FadeInRoutine() {
        string logId = "FadeInRoutine";
        logd(logId, "Starting FadeInRoutine");
        Color color = fadeImage.color;
        float alpha = 1.0f;

        while (alpha>0.0f) {
            alpha -= Time.deltaTime/fadeTime;
            color.a = alpha;
            fadeImage.color = color;
            yield return null;
        }
        logd(logId, "FadeInRoutine Ended");
    }
    public void FadeOut(System.Action callback=null) {
        StartCoroutine(FadeOutRoutine(callback));
    }
    public IEnumerator FadeOutRoutine(System.Action callback=null) {
        string logId = "FadeOutRoutine";
        logd(logId, "Starting FadeOutRoutine");
        Color color = fadeImage.color;
        float alpha = 0.0f;
        while (alpha < 1.0f) {
            alpha += Time.deltaTime/fadeTime;
            color.a = alpha;
            fadeImage.color = color;
            yield return null;
        }
        logd(logId, "FadeOutRoutine Ended");
        callback?.Invoke();
    }
}
