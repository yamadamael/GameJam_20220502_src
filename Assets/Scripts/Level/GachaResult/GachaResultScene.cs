using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gamejam
{
    public class GachaResultScene : BaseScene
    {
        public enum GachaResultState
        {
            Popup,  // ふわふわ～*10で光らせる
            Change, // 色変化
            Flying, // そして宇宙へ
            Result, // 結果画面
            End,
        }

        [SerializeField]
        GachaResultRoot _root;
        [SerializeField]
        GachaResultUIRoot _uiRoot;

        GachaResultState _state = GachaResultState.Popup;
        bool _enableInput = true;
        List<GachaResultInfo> _resultInfos;

        void Awake()
        {
            App.FadeManager.FadeIn(0.3f);
        }

        // Start is called before the first frame update
        void Start()
        {
            _resultInfos = (List<GachaResultInfo>)ParamMap["resultInfos"];

            _root.Init(ParamMap, SetNextState);
            _uiRoot.Init(ParamMap, SetNextState);

            _root.SetState(_state);
            _uiRoot.SetState(_state);
        }

        // Update is called once per frame
        void Update()
        {
        }

        void SetNextState()
        {
            if (_root.IsFinishEffect() && _uiRoot.IsFinishEffect())
            {
                _state++;

                if (_state == GachaResultState.Change)
                {
                    var isExistSR = false;
                    foreach (var resultInfo in _resultInfos)
                    {
                        var charaModel = App.ModelManager.CharaModels[resultInfo.CharaId];
                        if (Define.RaritySR <= charaModel.Rarity)
                        {
                            isExistSR = true;
                            break;
                        }
                    }

                    if (!isExistSR)
                    {
                        _state++;
                    }
                }

                Debug.Log(_state);
                _root.SetState(_state);
                _uiRoot.SetState(_state);
            }
            else
            {
                _root.EndEffect();
                _uiRoot.EndEffect();
            }
        }
    }
}
