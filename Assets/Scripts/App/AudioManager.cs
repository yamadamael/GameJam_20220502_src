using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace gamejam
{
    public class AudioManager : MonoBehaviour
    {
        public class AudioClipInfo
        {
            public string Path;
            public AudioClip Clip;
        }

        public class AudioSourceInfo
        {
            public int Index;
            public bool IsPlaying;
            public AudioSource Source;
        }

        List<AudioClip> sound;
        List<AudioSourceInfo> seSourceInfos;
        AudioSource bgmAudioSource;

        private float _seVolume = 0.2f;
        private float _bgmVolume = 0.2f;
        public float SEVolume => _seVolume;
        public float BGMVolume => _bgmVolume;

        public int position = 0;
        public int samplerate = 44100;
        public float frequency = 440;
        private List<AudioClipInfo> audioInfos;

        private void Awake()
        {
            seSourceInfos = new List<AudioSourceInfo>();
            // 4個で初期化
            for (var i = 0; i < 4; i++)
            {
                AddSESources();
            }
            bgmAudioSource = gameObject.AddComponent<AudioSource>();
            bgmAudioSource.volume = _bgmVolume;
            audioInfos = new List<AudioClipInfo>();
            PreLoad();
        }

        public AudioSourceInfo PlaySE(string path, float volume = 1f, UnityAction compCallback = null)
        {
            var info = FindSE(path);
            if (info == null)
            {
                // Debug.Log("Load: " + path);
                info = AddSE(path);
            }
            var seSourceInfo = GetFreeSESource();
            seSourceInfo.IsPlaying = true;
            StartCoroutine(seSourceInfo.Source.PlayWithCompCallback(info.Clip, volume,
                () =>
                {
                    seSourceInfo.IsPlaying = false;
                    compCallback?.Invoke();
                }));

            return seSourceInfo;
        }

        public void PlayBGM(string path, bool isLoop, float volume = 1f)
        {
            var ac = Resources.Load<AudioClip>(path);
            bgmAudioSource.Stop();
            bgmAudioSource.loop = isLoop;
            StartCoroutine(bgmAudioSource.PlayWithCompCallback(ac, volume));
        }

        public void StopBGM()
        {
            bgmAudioSource.Stop();
        }

        private AudioClipInfo FindSE(string path)
        {
            foreach (var info in audioInfos)
            {
                if (info.Path == path)
                {
                    return info;
                }
            }
            return null;
        }

        private AudioClipInfo AddSE(string path)
        {
            var clip = Resources.Load<AudioClip>(path);
            var info = new AudioClipInfo
            {
                Path = path,
                Clip = clip,
            };
            audioInfos.Add(info);
            return info;
        }

        private AudioSourceInfo GetFreeSESource()
        {
            foreach (var info in seSourceInfos)
            {
                if (!info.IsPlaying)
                {
                    // Debug.Log(info.Index);
                    return info;
                }
            }
            return AddSESources();
        }

        private AudioSourceInfo AddSESources()
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.volume = _seVolume;
            var sourceInfo = new AudioSourceInfo();
            sourceInfo.Source = source;
            sourceInfo.Index = seSourceInfos.Count;
            seSourceInfos.Add(sourceInfo);
            // Debug.Log(string.Format("AddSESources: {0}", seSourceInfos.Count));
            return sourceInfo;
        }

        private void PreLoad()
        {
            // TODO
            // var seList = GameModel.SEList;
            var seList = new List<string>();
            foreach (var se in seList)
            {
                AddSE(se);
            }
        }

        public void SetSEVolume(float volume)
        {
            _seVolume = volume;
            foreach (var sourceInfo in seSourceInfos)
            {
                sourceInfo.Source.volume = volume;
            }
        }

        public void SetBGMVolume(float volume)
        {
            _bgmVolume = volume;
            bgmAudioSource.volume = volume;
        }
    }
}
