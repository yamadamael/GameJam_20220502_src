using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace gamejam
{
    public class GachaResultYasai : MonoBehaviour
    {
        [SerializeField]
        SpriteRenderer _bg;
        [SerializeField]
        SpriteRenderer _overBg;
        [SerializeField]
        Animator _animator;

        GachaResultScene.GachaResultState _state;
        GachaResultInfo _resultInfo;
        bool _isFinishEffect;
        Sequence _sequence;
        int _index;
        Action _onFinishEvent;

        public void Init(GachaResultScene.GachaResultState state, GachaResultInfo resultInfo, int index, Action onFinishEvent)
        {
            _state = state;
            _resultInfo = resultInfo;
            _isFinishEffect = resultInfo == null;
            _index = index;
            _onFinishEvent = onFinishEvent;
        }

        void Update()
        {
            var color = _overBg.color;
            color.a = (Mathf.Sin(Time.time) + 1) / 2;
            _overBg.color = color;
        }

        public void SetState(GachaResultScene.GachaResultState state)
        {
            _state = state;
        }

        // アニメから呼ぶ用(なぜかboolを引数にできない)
        public void SetFinishEffect(int isFinish)
        {
            _isFinishEffect = isFinish != 0;
        }

        public void StartEffect()
        {
            if (GachaResultScene.GachaResultState.End <= _state)
            {
                return;
            }

            _isFinishEffect = false;

            switch (_state)
            {
                case GachaResultScene.GachaResultState.Popup:
                    StartPopup();
                    break;
                case GachaResultScene.GachaResultState.Change:
                    StartChange();
                    break;
                case GachaResultScene.GachaResultState.Flying:
                    StartFlying();
                    break;
            }
        }

        public void StartPopup()
        {
            _sequence = DOTween.Sequence();
            _sequence.AppendInterval(0.5f * (_index + 1));
            _sequence.AppendCallback(() =>
            {
                if (this != null && !_isFinishEffect)
                {
                    Popup();
                    _isFinishEffect = true;
                }
            });
            _sequence.Play();
        }

        public void Popup(bool isEndEffect = false)
        {
            gameObject.SetActive(true);

            if (!isEndEffect)
            {
                App.AudioManager.PlaySE("Sound/apper");
            }
        }

        public void StartChange()
        {
            var charaId = _resultInfo.CharaId;
            var chara = App.ModelManager.CharaModels[charaId];
            if (Define.RaritySR <= chara.Rarity)
            {
                _animator.Play("ResultYasaiChange");
            }
            else
            {
                _isFinishEffect = true;
            }
        }

        public void ChangeBGColor()
        {
            var charaId = _resultInfo.CharaId;
            var chara = App.ModelManager.CharaModels[charaId];
            switch (chara.Rarity)
            {
                case Define.RaritySR:
                    _bg.color = Color.blue;
                    break;
                case Define.RaritySSR:
                    _bg.color = Color.green;
                    break;
                case Define.RaritySSSR:
                    _bg.color = Color.green;
                    break;
            }
        }

        public void OnFinishChange()
        {
            _animator.Play("ResultYasaiDefault");
            var charaId = _resultInfo.CharaId;
            var chara = App.ModelManager.CharaModels[charaId];
            switch (chara.Rarity)
            {
                case Define.RaritySR:
                    _overBg.color = Color.blue;
                    break;
                case Define.RaritySSR:
                    _overBg.color = Color.green;
                    break;
                case Define.RaritySSSR:
                    _overBg.color = Color.green;
                    break;
            }
        }

        public void StartFlying()
        {
            var position = transform.position;

            _sequence = DOTween.Sequence();
            _sequence.AppendInterval(0.1f * (_index + 1));
            _sequence.AppendCallback(() =>
            {
                App.AudioManager.PlaySE("Sound/shot");
            });
            _sequence.Append(gameObject.transform.DOMoveY(position.y + 10, 0.2f).SetEase(Ease.OutQuad));
            _sequence.AppendCallback(() =>
            {
                if (this != null)
                {
                    _isFinishEffect = true;
                    _onFinishEvent.Invoke();
                }
            });
            _sequence.Play();
        }

        public void Flying()
        {

        }

        public void EndEffect()
        {
            switch (_state)
            {
                case GachaResultScene.GachaResultState.Popup:
                    if (!_isFinishEffect)
                    {
                        _sequence.Pause();
                        _sequence.Kill();
                        _isFinishEffect = true;
                        Popup(true);
                    }
                    break;
                case GachaResultScene.GachaResultState.Change:
                    ChangeBGColor();
                    OnFinishChange();
                    _isFinishEffect = true;
                    break;
                case GachaResultScene.GachaResultState.Flying:
                    _isFinishEffect = true;
                    _onFinishEvent.Invoke();
                    break;
            }
        }

        public bool IsFinishEffect()
        {
            return _resultInfo == null || (_isFinishEffect && gameObject.activeSelf);
        }
    }
}
