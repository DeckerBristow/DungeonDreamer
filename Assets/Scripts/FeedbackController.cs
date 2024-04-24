using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FeedbackController : MonoBehaviour {
    private TextMeshProUGUI text;
    private Color originalColor;
    public float fadeDuration = 1.5f;
    private float fadeTimer = 0f;
    private float lastFadeTime;
    void Start () {
        text = GetComponent<TextMeshProUGUI>();
        originalColor = text.color;
        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
    }
    void Update () {
        fadeTimer = 2.5f - (Time.realtimeSinceStartup - lastFadeTime);
        if (fadeTimer < 0f) {
            fadeTimer = 0f;
        }
        if (fadeTimer > 0f) {
            float alpha = originalColor.a*(fadeTimer/fadeDuration);
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        }
        else {
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
        }
    }
    public void ChangedText() {
        lastFadeTime = Time.realtimeSinceStartup;     
    }
    public IEnumerator Disappear() {
        float currentTime = 0;
        while (currentTime <= fadeDuration) {
            float alpha = 255*(1-currentTime/fadeDuration);
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
    }
    public void Activate() {
        text.color = originalColor;
        transform.SetAsLastSibling();
    }
}