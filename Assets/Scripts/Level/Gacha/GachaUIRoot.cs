using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace gamejam
{
    public class GachaUIRoot : MonoBehaviour
    {
        [SerializeField]
        Button _gacha1Button;
        [SerializeField]
        Button _gacha2Button;
        [SerializeField]
        Button _backButton;
        [SerializeField]
        GachaCharaCount[] _charaCounts;
        [SerializeField]
        GachaGachaRate[] _gachaRates;
        [SerializeField]
        GachaChara _gachaChara;
        [SerializeField]
        GachaGachaComplete _gachaComp;
        [SerializeField]
        Text _money;
        [SerializeField]
        GachaYasai _yasaiPrefab;
        [SerializeField]
        GameObject _bg;
        [SerializeField]
        Config _config;

        List<GachaYasai> _yasais;

        public void Init()
        {
            _gacha1Button.onClick.AddListener(() => OnClickGachaButton(1));
            _gacha2Button.onClick.AddListener(() => OnClickGachaButton(10));
            _backButton.onClick.AddListener(OnClickBackButton);
            UpdateCharaCount();
            UpdateGachaRate();
            _gachaChara.Init();
            _gachaComp.Init();
            _money.text = string.Format("{0:#,0}", App.UserDataManager.Data.Money);
            _config.Init();

            _yasais = new List<GachaYasai>();
            var yasaiCount = App.UserDataManager.UserChara.List.Count;
            yasaiCount = 200 < yasaiCount ? 200 : yasaiCount;
            for (var i = 0; i < yasaiCount; i++)
            {
                var yasai = Instantiate<GachaYasai>(_yasaiPrefab);
                yasai.transform.SetParent(_bg.transform);
                yasai.Init();
                _yasais.Add(yasai);
            }
        }

        void OnClickGachaButton(int count)
        {
            _gachaComp.KillSequence();

            var resultInfos = App.ModelManager.Gacha.DrawGacha(count);
            var paramList = new Dictionary<string, object>();
            paramList.Add("resultInfos", resultInfos);
            SceneManager.LoadSceneAsync(SceneConst.Scenes.GachaResult.ToString(), 0.3f, paramList);
        }

        void OnClickBackButton()
        {
            SceneManager.LoadSceneAsync(SceneConst.Scenes.Gacha.ToString(), 0.3f);
        }

        public void UpdateCharaCount()
        {
            var charaCounts = new Dictionary<int, int>();
            var charaMaxs = new Dictionary<int, int>();
            for (var i = 0; i < _charaCounts.Length; i++)
            {
                var rarity = i + 1;
                charaCounts.Add(rarity, 0);
                charaMaxs.Add(rarity, 0);
            }

            var charaList = App.ModelManager.CharaModels;
            foreach (var charaModel in charaList.Values)
            {
                var rarity = charaModel.Rarity;

                charaMaxs[rarity]++;
                if (App.UserDataManager.Data.IsExistGetList(charaModel.Id))
                {
                    charaCounts[rarity]++;
                }
            }

            for (var i = 0; i < _charaCounts.Length; i++)
            {
                var rarity = i + 1;
                _charaCounts[i].Init(rarity, charaCounts[rarity], charaMaxs[rarity]);
            }
        }

        public void UpdateGachaRate()
        {
            var rarityRateList = App.ModelManager.Gacha.RarityRateList;
            for (var i = 0; i < _gachaRates.Length; i++)
            {
                var rarity = i + 1;
                _gachaRates[i].Init(rarity, (int)rarityRateList[rarity].Rate / 10);
            }
        }

        public void UpdateUIRoot()
        {
            foreach (var yasai in _yasais)
            {
                yasai.Move();
            }

            _config.UpdateConfig();
        }
    }
}
