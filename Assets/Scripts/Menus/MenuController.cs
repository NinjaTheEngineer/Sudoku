using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : NinjaMonoBehaviour {
    public Vector3 activePosition;
    public Vector3 deactivatedPosition;
    public float animationTime = 0.5f;
    public MenuState CurrentState {
        get; protected set;
    }
    private void Awake() {
        deactivatedPosition.y = 0-Screen.height/2f-GetComponent<RectTransform>().sizeDelta.y;
    }
    public enum MenuState {
        Activating,
        Active,
        Deactivating,
        Deactivated
    }
    public virtual void Activate(Action<float> activateAnimation = null) {
        string logId = "Activate";
        gameObject.SetActive(true);
        if(activateAnimation==null) {
            logt(logId, "No animation for activating Menu="+name+" => Setting currentState to active");
            CurrentState = MenuState.Active;
        } else {
            CurrentState = MenuState.Activating;
            StartCoroutine(Animate(activateAnimation, () => CurrentState = MenuState.Active));
        }
    }

    public virtual void Deactivate(Action<float> deactivateAnimation = null) {
        string logId = "Deactivate";
        if(deactivateAnimation==null) {
            logt(logId, "No animation for deactivating Menu="+name+" => Setting currentState to deactivated");
            CurrentState = MenuState.Deactivated;
            gameObject.SetActive(false);
        } else {
            CurrentState = MenuState.Deactivating;
            StartCoroutine(Animate(deactivateAnimation, () => {
                gameObject.SetActive(false);
                CurrentState = MenuState.Deactivated; 
            }));
        }
    }

    private IEnumerator Animate(Action<float> animationFunction, Action callback=null) {
        float startTime = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup < startTime+animationTime) {
            float progress = (Time.realtimeSinceStartup-startTime) / animationTime;
            animationFunction(progress);
            yield return true;
        }
        callback?.Invoke();
    }
}
