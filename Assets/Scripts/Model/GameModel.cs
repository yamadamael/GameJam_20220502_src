using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using LitJson;

namespace gamejam
{
    public class GameModel
    {
        public List<CharaModel> CharaModels;

        public int Point;

        public GameModel()
        {
            InitCharaModels();
        }

        void InitCharaModels()
        {
            var path = "Json/CharaData";
            var jsonDatas = Util.LoadJson(path);

            CharaModels = new List<CharaModel>();
            var count = 9;
            var week = new int[7];
            for (var i = 0; i < count; i++)
            {
                var data = jsonDatas[i];
                var chara = new CharaModel();
                chara.Id = Convert.ToInt32((string)data["Id"]);
                chara.Rarity = Convert.ToInt32((string)data["Rarity"]);
                chara.Name = (string)data["NameSecond"];
                chara.Description = (string)data["NameFirstHira"];

                CharaModels.Add(chara);
            }
            Debug.Log(string.Join(",", week));
        }

        void UpdateChara(CharaModel chara)
        {
            foreach (var c in CharaModels)
            {
                if (chara == null || chara.Id != c.Id)
                {
                }
            }
        }
    }
}
