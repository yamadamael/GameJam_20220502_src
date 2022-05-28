using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace gamejam
{

    public class CharaIcon : MonoBehaviour
    {
        [SerializeField]
        Image _frame;
        [SerializeField]
        GameObject _new;
        [SerializeField]
        Image _charaImage;
        [SerializeField]
        GameObject _raritieBg;
        [SerializeField]
        GameObject[] _rarities;
        [SerializeField]
        Image _gradationImage;
        [SerializeField]
        GameObject _backLight;
        [SerializeField]
        GameObject _grayFilter;

        int _index;
        int _charaId;
        public int CharaId => _charaId;
        bool _isNew;
        Sequence _sequence;
        bool _isFinishEffect;
        Action _onFinishEffectEvent;

        public void Init(int index, int charaId, bool isNew, Action onFinishEffectEvent)
        {
            _index = index;
            _charaId = charaId;
            _isNew = isNew;
            _onFinishEffectEvent = onFinishEffectEvent;

            var charaModel = App.ModelManager.CharaModels[_charaId];
            SetRarity(true);
            SetFrameColor(charaModel.Rarity);

            var path = string.Format("Texture/{0}", charaModel.IconName);
            _charaImage.sprite = Resources.Load<Sprite>(path);

            _gradationImage.gameObject.SetActive(Define.RaritySR <= charaModel.Rarity);
            if (Define.RaritySR < charaModel.Rarity)
            {
                // _gradationImage.color = Color.blue;
            }

            _new.SetActive(isNew);
            _backLight.SetActive(false);
            _grayFilter.SetActive(false);
        }

        void SetFrameColor(int rarity)
        {
            var color = new Color();
            switch (rarity)
            {
                case Define.RarityR:
                    color = Color.gray;
                    break;
                case Define.RaritySR:
                    color = Color.blue;
                    break;
                case Define.RaritySSR:
                    color = Color.yellow;
                    break;
                case Define.RaritySSSR:
                    color = Color.red;
                    break;
            }

            _frame.color = color;
        }

        public void Show()
        {
            var position = transform.position;
            transform.position = new Vector3(position.x, position.y - 10, position.z);

            gameObject.SetActive(true);

            _sequence = DOTween.Sequence();
            _sequence.AppendInterval(0.1f * (_index + 1));
            _sequence.Append(gameObject.transform.DOMoveY(position.y, 0.2f).SetEase(Ease.OutQuad));
            _sequence.AppendCallback(() =>
            {
                if (this != null)
                {
                    _isFinishEffect = true;
                    _onFinishEffectEvent.Invoke();

                    var charaModel = App.ModelManager.CharaModels[_charaId];
                    if (Define.RaritySSR <= charaModel.Rarity)
                    {
                        _backLight.SetActive(true);
                    }

                    App.AudioManager.PlaySE("Sound/fit");
                }
            });
            _sequence.Play();
        }

        public bool IsFinishEffect()
        {
            return _isFinishEffect;
        }

        public void SetGrayFilter(bool isOn)
        {
            _grayFilter.SetActive(isOn);
        }

        public void KillSequence()
        {
            _sequence.Kill();
        }

        public void SetBeatAnimation()
        {
            var rt = gameObject.GetComponent<RectTransform>();

            transform.localScale = Vector3.one * 0.8f;
            _sequence = DOTween.Sequence();
            _sequence.Append(rt.DOScale(Vector3.one * 1f, 0.1f));
            _sequence.Append(rt.DOScale(Vector3.one * 0.8f, 0.2f));
            _sequence.SetLoops(-1);
            _sequence.Play();
        }

        public void SetRarity(bool isOn)
        {
            _raritieBg.SetActive(isOn);
            var charaModel = App.ModelManager.CharaModels[_charaId];
            for (var i = 0; i < _rarities.Length; i++)
            {
                _rarities[i].SetActive(charaModel.Rarity == i + 1 && isOn);
            }
        }
    }
}
