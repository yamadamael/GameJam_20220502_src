using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gamejam
{
    public class GachaResultHatake : MonoBehaviour
    {
        [SerializeField]
        GachaResultYasai[] _yasais;

        GachaResultScene.GachaResultState _state;
        List<GachaResultInfo> _resultInfos;
        Action _onFinishEvent;

        public void Init(GachaResultScene.GachaResultState state, List<GachaResultInfo> resultInfos, Action onFinishEvent)
        {
            _state = state;
            _resultInfos = resultInfos;
            _onFinishEvent = onFinishEvent;

            for (var i = 0; i < _yasais.Length; i++)
            {
                _yasais[i].gameObject.SetActive(false);

                if (i < resultInfos.Count)
                {
                    _yasais[i].Init(_state, resultInfos[i], i, OnFinishEvent);
                    _yasais[i].StartEffect();
                }
            }
        }

        public void SetState(GachaResultScene.GachaResultState state)
        {
            _state = state;

            if (GachaResultScene.GachaResultState.Result <= state)
            {
                gameObject.SetActive(false);
                return;
            }

            for (var i = 0; i < _yasais.Length; i++)
            {
                if (i < _resultInfos.Count)
                {
                    _yasais[i].SetState(state);
                    _yasais[i].StartEffect();
                }
            }
        }

        public void EndEffect()
        {
            for (var i = 0; i < _yasais.Length; i++)
            {
                _yasais[i].EndEffect();
            }
        }

        public bool IsFinishEffect()
        {
            foreach (var yasai in _yasais)
            {
                if (!yasai.IsFinishEffect())
                {
                    return false;
                }
            }

            return true;
        }

        public void OnFinishEvent()
        {
            if (IsFinishEffect())
            {
                _onFinishEvent.Invoke();
            }
        }
    }
}
