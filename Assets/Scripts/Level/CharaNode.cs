using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace gamejam
{
    public class CharaNode : MonoBehaviour
    {
        [SerializeField]
        Button _button;

        [SerializeField]
        Text _name;
        [SerializeField]
        Text _week;
        [SerializeField]
        Text _friendly;
        [SerializeField]
        Image _heartImage;
        [SerializeField]
        Text _stress;

        [SerializeField]
        Image _face;
        [SerializeField]
        Image _filter;
        [SerializeField]
        Image _back;
        [SerializeField]
        Image _explode;

        public Action<CharaModel> NextDayCallback;
        public Action<float, Action, CharaModel> DeadCallback;

        CharaModel _charaModel;

        public void Init(CharaModel chara)
        {
            _button.onClick.AddListener(OnClickNodeButton);
        }

        public void UpdateNode(int maxStress)
        {
        }

        void OnClickNodeButton()
        {
        }
    }
}
