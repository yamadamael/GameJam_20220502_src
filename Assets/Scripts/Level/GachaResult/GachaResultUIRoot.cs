using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace gamejam
{
    public class GachaResultUIRoot : MonoBehaviour
    {
        [SerializeField]
        Button _button;
        [SerializeField]
        GachaResult _result;

        GachaResultScene.GachaResultState _state;
        List<GachaResultInfo> _resultInfos;

        public void Init(Dictionary<string, object> paramMap, Action onClickEvent)
        {
            _button.onClick.AddListener(() => onClickEvent.Invoke());

            _resultInfos = (List<GachaResultInfo>)paramMap["resultInfos"];
            _result.gameObject.SetActive(false);
            _result.Init(_resultInfos);
        }

        public void SetState(GachaResultScene.GachaResultState state)
        {
            _state = state;
            StartEffect();
        }

        public void StartEffect()
        {
            switch (_state)
            {
                case GachaResultScene.GachaResultState.Popup:
                case GachaResultScene.GachaResultState.Change:
                case GachaResultScene.GachaResultState.Flying:
                    return;
                case GachaResultScene.GachaResultState.Result:
                    _result.Show();
                    _button.gameObject.SetActive(false);
                    break;
            }
        }

        public void EndEffect()
        {
        }

        public bool IsFinishEffect()
        {
            return true;
        }
    }
}
