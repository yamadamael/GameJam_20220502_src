using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using LitJson;

namespace gamejam
{
    public class ModelManager
    {
        public Dictionary<int, CharaModel> CharaModels;
        public GachaModel Gacha;

        public int Point;

        public ModelManager()
        {
            CharaModels = new Dictionary<int, CharaModel>();
            Gacha = new GachaModel();

        }
        public void Init()
        {
            InitCharaModels();
            InitGachaModel();
        }

        void InitCharaModels()
        {
            var path = "GameData/Chara";
            var jsonDatas = Util.LoadJson(path);
            if (jsonDatas == null)
            {
                Debug.Log(string.Format("load miss: ", path));
                return;
            }

            var count = jsonDatas.Count;
            for (var i = 0; i < count; i++)
            {
                var data = jsonDatas[i];
                var chara = new CharaModel();
                chara.Id = (int)data["Id"];
                chara.Rarity = (int)data["Rarity"];
                chara.Name = (string)data["Name"];
                chara.Description = (string)data["Description"];
                chara.IconName = (string)data["IconName"];

                CharaModels.Add(chara.Id, chara);
            }
        }

        void InitGachaModel()
        {
            Gacha.Init();
        }

        void UpdateChara(CharaModel chara)
        {
            foreach (var c in CharaModels.Values)
            {
                if (chara == null || chara.Id != c.Id)
                {
                }
            }
        }
    }
}
