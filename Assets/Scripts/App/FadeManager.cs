using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace gamejam
{
    public class FadeManager : MonoBehaviour
    {
        [SerializeField]
        private Image _faderImage = null;

        public void Awake()
        {
            _faderImage.color = new Color(0, 0, 0, 0);
        }

        public void FadeOut(float time, Action fadeOutEvent = null)
        {
            StartCoroutine(Fade(0, 1, time, fadeOutEvent));
        }

        public void FadeIn(float time, Action fadeInEvent = null)
        {
            StartCoroutine(Fade(1, 0, time, fadeInEvent));
        }

        private IEnumerator Fade(float startAlpha, float endAlpha, float time, Action fadeEvent)
        {
            _faderImage.gameObject.SetActive(true);
            var startTime = Time.time;
            while (true)
            {
                var currentTime = Time.time - startTime;
                if (currentTime > time)
                {
                    _faderImage.color = new Color(_faderImage.color.r, _faderImage.color.g, _faderImage.color.b, endAlpha);
                    _faderImage.gameObject.SetActive(endAlpha > 0);
                    fadeEvent?.Invoke();
                    yield break;
                }
                var leapAlpha = LeapAlpha(startAlpha, endAlpha, time, currentTime);
                _faderImage.color = new Color(_faderImage.color.r, _faderImage.color.g, _faderImage.color.b, leapAlpha);
                yield return null;
            }
        }

        private float LeapAlpha(float startAlpha, float endAlpha, float time, float currentTime)
        {
            return startAlpha + ((endAlpha - startAlpha) * (currentTime / time));
        }
    }
}
