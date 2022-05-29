using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace gamejam
{
    public static class AudioSourceExtension
    {
        public static IEnumerator PlayWithCompCallback(this AudioSource audioSource, AudioClip audioClip, float volume = 1f, UnityAction compCallback = null)
        {
            audioSource.clip = audioClip;
            audioSource.volume = volume / 2 * App.AudioManager.SEVolume;
            audioSource.Play();
            float timer = 0f;

            if (audioClip != null)
            {
                //WaitForSecondsを使うとCoroutineを一時停止・再開できなくなるのでwhileで対応//
                while (timer < audioClip.length)
                {
                    timer += Time.deltaTime;
                    yield return null;
                }
            }

            if (compCallback != null)
            {
                compCallback();
            }
        }

        public static IEnumerator PlayOneShotWithCompCallback(this AudioSource audioSource, AudioClip audioClip, float volume = 1f, UnityAction compCallback = null)
        {
            audioSource.volume = volume;
            audioSource.PlayOneShot(audioClip);
            float timer = 0f;

            if (audioClip != null)
            {
                //WaitForSecondsを使うとCoroutineを一時停止・再開できなくなるのでwhileで対応//
                while (timer < audioClip.length)
                {
                    timer += Time.deltaTime;
                    yield return null;
                }
            }

            if (compCallback != null)
            {
                compCallback();
            }
        }
    }
}
