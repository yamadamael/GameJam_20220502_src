using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace gamejam
{
    public class Config : MonoBehaviour
    {
        [SerializeField]
        CustomSlider _bgmSlider;
        [SerializeField]
        CustomSlider _seSlider;

        float _lastPlaySETime;
        const float _playSEWaitTime = 0.5f;

        public void Init()
        {
            _bgmSlider.value = App.AudioManager.BGMVolume;
            _seSlider.value = App.AudioManager.SEVolume;

            _bgmSlider.onValueChanged.AddListener((volume) =>
            {
                App.AudioManager.SetBGMVolume(volume);
            });

            _seSlider.onValueChanged.AddListener((volume) =>
            {
                App.AudioManager.SetSEVolume(volume);

            });
        }

        public void UpdateConfig()
        {
            if (_seSlider.IsPointerDown)
            {
                if (_lastPlaySETime + _playSEWaitTime < Time.time)
                {
                    // Debug.Log($"{_lastPlaySETime}, {Time.time}, {_playSEWaitTime}, {Time.time + _playSEWaitTime}");
                    App.AudioManager.PlaySE("Sound/apper");
                    _lastPlaySETime = Time.time;
                }
            }
        }
    }
}
