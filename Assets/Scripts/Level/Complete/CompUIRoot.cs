using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace gamejam
{
    public class CompUIRoot : MonoBehaviour
    {
        public enum CompState
        {
            Appear,
            Rotate_Near,
            Ascension,
            Diffusion,
            Fall,
            UIDisp,
        }

        [SerializeField]
        Button _tweetButton;
        [SerializeField]
        Button _rankingButton;
        [SerializeField]
        Button _backButton;

        [SerializeField]
        GameObject _targetParent;

        [SerializeField]
        CharaIcon[] _targetIcons;

        [SerializeField]
        CharaIcon _rewardIcon;

        [SerializeField]
        GameObject _fallPos;

        [SerializeField]
        Image[] _pillar;

        [SerializeField]
        Image _light;

        Sequence _sequence;
        CompState _state;
        bool _isFinishEffect;
        float _t;

        List<AudioManager.AudioSourceInfo> _swingASIs;
        AudioManager.AudioSourceInfo _fallASI;

        public void Init()
        {
            var score = App.UserDataManager.Data.ClearMoney;
            _tweetButton.onClick.AddListener(() => OnClickTweetButton(score));
            _rankingButton.onClick.AddListener(() => OnClickScoreButton(score));
            _backButton.onClick.AddListener(OnClickBackButton);
            _tweetButton.transform.parent.gameObject.SetActive(false);

            _swingASIs = new List<AudioManager.AudioSourceInfo>();

            _rewardIcon.Init(0, App.ModelManager.Gacha.Complete.CompReward, false, null);
            _rewardIcon.SetRarity(false);

            _sequence = DOTween.Sequence();
            var targetList = App.ModelManager.Gacha.Complete.CompTargets;
            var count = _targetIcons.Length;
            for (var i = 0; i < count; i++)
            {
                var icon = _targetIcons[i];
                icon.Init(0, targetList[i], false, null);
                icon.SetRarity(false);
                icon.gameObject.SetActive(false);

                var ii = i;
                _sequence.AppendInterval(0.5f);
                _sequence.AppendCallback(() =>
                {
                    icon.gameObject.SetActive(true);
                    App.AudioManager.PlaySE("Sound/apper");
                });
            }

            foreach (var p in _pillar)
            {
                p.gameObject.SetActive(false);
                p.transform.localPosition = Vector3.zero;
            }

            _light.gameObject.SetActive(false);
        }

        void OnClickBackButton()
        {
            App.AudioManager.PlayBGM("Sound/BGM_Theme", true, 0.75f);
            SceneManager.LoadSceneAsync(SceneConst.Scenes.Gacha.ToString(), 0.3f);
        }

        private void OnClickTweetButton(int score)
        {
            var text = $"今やってるゲームはこれ！ガチャに{score}円溶かしたよ！";
            var url = "flyingyasaigarl";
            var tags = new string[] { "unity1week" };
            naichilab.UnityRoomTweet.Tweet(url, text, tags);
        }

        private void OnClickScoreButton(int score)
        {
            // ニフクラはasurasurasuraアカウント
            // https://naichilab.hatenablog.com/entry/webgl-simple-ranking
            // ランキング出したいシーンに
            //  NCMBSettingsのコンポーネントを置いて2種類のキーを設定する
            //  Assets/naichilab/unity-simple-ranking/Prefabs/RankingLoader.prefabを置く
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(score);
            naichilab.RankingSceneManager.IsOpened = true;
        }

        public void UpdateUI()
        {
            switch (_state)
            {
                case CompState.Appear:
                    {
                        foreach (var icon in _targetIcons)
                        {
                            if (!icon.gameObject.activeSelf)
                            {
                                return;
                            }
                        }

                        if (1 < _t)
                        {
                            _sequence.Kill();
                            NextState();
                            InitRotateNear();
                            return;
                        }
                    }
                    break;
                case CompState.Rotate_Near:
                    {
                        if (2 < _t)
                        {
                            NextState();
                            InitAscention();
                            return;
                        }

                        _targetParent.transform.Rotate(0, 0, 100);

                        if (1 < _t)
                        {
                            // icon近づく
                            var t = _t - 1;
                            var a = t / 1f;
                            foreach (var icon in _targetIcons)
                            {
                                var parentPos = icon.transform.parent.position;
                                var diff = _targetParent.transform.position - parentPos;
                                icon.transform.position = parentPos + (diff * a);
                                // icon.transform.rotation = Quaternion.identity;
                            }
                        }
                    }
                    break;
                case CompState.Ascension:
                    {
                        _targetParent.transform.Rotate(0, 0, 100);

                        var count = _pillar.Length;
                        var span = 0.03f;
                        var loopTime = (span * (count / 2)) + 0.3f;
                        var t = _t % loopTime;
                        for (var i = 0; i < count / 2; i++)
                        {
                            if (i < t / span)
                            {
                                for (var ii = i * 2; ii < i * 2 + 2; ii++)
                                {
                                    var dir = (ii % 2) == 0 ? 1 : -1;
                                    var pos = _pillar[ii].transform.localPosition;
                                    _pillar[ii].gameObject.SetActive(true);
                                    _pillar[ii].transform.localPosition = new Vector3(pos.x + (540 * (Time.deltaTime / 0.3f) * dir), 0, 0);
                                }
                            }
                        }

                        var loopCount = (int)(_t / loopTime);
                        var loopCount2 = (int)((_t + Time.deltaTime) / loopTime);

                        if (loopCount < loopCount2)
                        {
                            foreach (var p in _pillar)
                            {
                                p.gameObject.SetActive(false);
                                p.transform.localPosition = Vector3.zero;
                            }

                            if (loopCount2 == 1)
                            {
                                App.AudioManager.PlaySE("Sound/shakin1");
                            }
                            else if (loopCount2 == 2)
                            {
                                App.AudioManager.PlaySE("Sound/shakin2");
                            }
                        }

                        if (2 <= loopCount2)
                        {
                            NextState();
                            return;
                        }
                    }
                    break;
                case CompState.Diffusion:
                    {
                        _targetParent.transform.Rotate(0, 0, 100);

                        const int lightUp = 1;
                        const int fadeOut = 3;
                        if (_t < lightUp)
                        {
                            var alpha = _t;
                            _light.gameObject.SetActive(true);
                            _light.transform.localScale = Vector3.one * alpha;
                            var c = _light.color;
                            c.a = alpha;
                            _light.color = c;

                            if (lightUp <= _t + Time.deltaTime)
                            {
                                foreach (var p in _pillar)
                                {
                                    p.gameObject.SetActive(false);
                                    p.transform.localPosition = Vector3.zero;
                                }
                                foreach (var icon in _targetIcons)
                                {
                                    icon.gameObject.SetActive(false);
                                }
                            }
                        }
                        else if (_t < fadeOut)
                        {
                            var alpha = _t / 2f;
                            var c = _light.color;
                            c.a = 1 - alpha;
                            _light.color = c;

                            if (fadeOut <= _t + Time.deltaTime)
                            {
                                NextState();
                                InitFall();
                                return;
                            }
                        }
                    }
                    break;
                case CompState.Fall:
                    // InitFall()を実行している
                    break;
                case CompState.UIDisp:
                    _tweetButton.transform.parent.gameObject.SetActive(true);
                    NextState();
                    break;
            }

            _t += Time.deltaTime;
        }

        void NextState()
        {
            _state++;
            Debug.Log(_state);
            _t = 0;
        }

        void InitRotateNear()
        {
            _sequence = DOTween.Sequence();
            var interval = 0.3f;
            _sequence.AppendCallback(() =>
            {
                _swingASIs.Add(App.AudioManager.PlaySE("Sound/swing4"));
            });
            _sequence.AppendInterval(interval);
            _sequence.AppendCallback(() =>
            {
                _swingASIs.Add(App.AudioManager.PlaySE("Sound/swing4"));
            });
            _sequence.AppendInterval(interval);
            _sequence.AppendCallback(() =>
            {
                _swingASIs.Add(App.AudioManager.PlaySE("Sound/swing4"));
            });
            _sequence.OnComplete(() =>
            {
            });
            _sequence.Play();
        }

        void InitAscention()
        {
            foreach (var asi in _swingASIs)
            {
                asi.Source.Stop();
            }

            App.AudioManager.PlaySE("Sound/shakin1");
        }

        void InitFall()
        {
            const int FallTime = 5;
            var parent = _rewardIcon.transform.parent;
            _sequence = DOTween.Sequence();
            _sequence.AppendCallback(() =>
            {
                _rewardIcon.transform.DOShakePosition(FallTime + 1, 10);
                parent.DOMoveY(540 / 2, FallTime);
            });
            _sequence.AppendInterval(FallTime);
            _sequence.OnComplete(() =>
            {
                _fallASI.Source.Stop();
                NextState();
            });
            _sequence.Play();

            _fallASI = App.AudioManager.PlaySE("Sound/gogogo");
        }
    }
}
