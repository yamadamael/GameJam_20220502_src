using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace gamejam
{
    public class HomeUIRoot : MonoBehaviour
    {
        // ボタンたち
        [SerializeField]
        Button _charaButton;
        [SerializeField]
        Button _gachaButton;
        [SerializeField]
        Button _mypageButton;
        [SerializeField]
        Button _levelupButton;
        [SerializeField]
        Button _missionButton;

        // ホームキャラ
        [SerializeField]
        Image _charaImage;

        // リソース系(静的に持ちたい)
        // 石
        // マネー
        // スタミナ

        [SerializeField]
        EventSystem _eventSystem;
        [SerializeField]
        Canvas _canvas;

        public void Init()
        {
            _charaButton.onClick.AddListener(OnClickCharaButton);
            _gachaButton.onClick.AddListener(OnClickGachaButton);
            _missionButton.onClick.AddListener(OnClickMissionButton);
            _mypageButton.onClick.AddListener(OnClickDummyButton);
            _levelupButton.onClick.AddListener(OnClickDummyButton);
        }

        void UpdateUI()
        {
            if (false)
            {
                Debug.Log("ゲームクリア");
                SceneManager.LoadScene(SceneConst.Scenes.GameClear.ToString(), 0);
            }
        }

        void OnClickCharaButton()
        {
        }

        void OnClickGachaButton()
        {
            SceneManager.LoadScene(SceneConst.Scenes.Gacha.ToString(), 0.3f);
        }

        void OnClickMissionButton()
        {
            // TODO
            OnClickDummyButton();
        }

        void OnClickDummyButton()
        {
            // systemmessage "ただいまサービスを見合わせております。誠に申し訳ございません。"
        }
    }
}
