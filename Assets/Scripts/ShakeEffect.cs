using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeEffect : NinjaMonoBehaviour {
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    public float shakeMagnitude = 0.05f;
    public float shakeTime = 0.1f;
    void Start() {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
    }
    public void Shake() {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine() {
        float elapsed = 0.0f;
        while (elapsed < shakeTime) {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;
            transform.position = _initialPosition + new Vector3(x, y, 0);
            transform.rotation = _initialRotation * Quaternion.Euler(0, 0, Random.Range(-1f, 1f) * shakeMagnitude * 90f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;
    }
}