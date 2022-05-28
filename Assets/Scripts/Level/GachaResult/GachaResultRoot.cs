using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gamejam
{
    public class GachaResultRoot : MonoBehaviour
    {
        [SerializeField]
        GachaResultHatake _hatake;
        [SerializeField]
        SpriteRenderer _bgGround;
        [SerializeField]
        SpriteRenderer _bgSpace;

        GachaResultScene.GachaResultState _state;
        List<GachaResultInfo> _resultInfos;

        public void Init(Dictionary<string, object> paramMap, Action onFinishEvent)
        {
            _resultInfos = (List<GachaResultInfo>)paramMap["resultInfos"];
            _hatake.Init(_state, _resultInfos, onFinishEvent);
            _bgGround.gameObject.SetActive(true);
            _bgSpace.gameObject.SetActive(false);
        }

        public void SetState(GachaResultScene.GachaResultState state)
        {
            _state = state;
            _hatake.SetState(state);

            if (state == GachaResultScene.GachaResultState.Result)
            {
                _bgGround.gameObject.SetActive(false);
                _bgSpace.gameObject.SetActive(true);
            }
        }

        public void EndEffect()
        {
            _hatake.EndEffect();
        }

        public bool IsFinishEffect()
        {
            return _hatake.IsFinishEffect();
        }
    }
}
