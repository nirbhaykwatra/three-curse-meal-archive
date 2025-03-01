using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _holdDuration;

    private Image _black;
    private void Start()
    {
        _black = GetComponent<Image>();
        StartCoroutine(FadeOut());
    }

    public void StartFade()
    {
        StartCoroutine(Fade());
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    public void FadeWait()
    {
        StartCoroutine(FadeDuration());
    }

    private IEnumerator FadeDuration()
    {
        yield return new WaitForSeconds(_holdDuration);

        StartCoroutine(FadeOut());
    }

    private IEnumerator Fade()
    {
        StartCoroutine(FadeIn());

        yield return new WaitForSeconds(_holdDuration);

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        Color _blackColor = _black.color;
        float fadeAmount;
        
        while (_black.color.a < 1)
        {

            fadeAmount = _blackColor.a + (_duration * Time.deltaTime);

            _blackColor = new Color(_blackColor.r, _blackColor.g, _blackColor.b, fadeAmount);
            _black.color = _blackColor;
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        Color _blackColor = _black.color;
        float fadeAmount;
        
        while (_black.color.a > 0)
        {
            fadeAmount = _blackColor.a - (_duration * Time.deltaTime);
                
            _blackColor = new Color(_blackColor.r, _blackColor.g, _blackColor.b, fadeAmount);
            _black.color = _blackColor;
            yield return null;
        }
    }
}
