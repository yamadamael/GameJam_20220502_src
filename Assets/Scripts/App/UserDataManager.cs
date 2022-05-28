using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using LitJson;

namespace gamejam
{
    public class UserDataManager
    {
        public UserDataModel Data;
        public UserCharaList UserChara;

        public int Point;

        public UserDataManager()
        {
            Data = new UserDataModel();
            UserChara = new UserCharaList();
        }

        public void Init()
        {
            InitUserCharaList();
        }

        void InitUserCharaList()
        {
            // var path = "GameData/Chara";
            // var jsonDatas = Util.LoadJson(path);
            // if (jsonDatas == null)
            // {
            //     Debug.Log(string.Format("load miss: ", path));
            //     return;
            // }
            // CharaModels = new Dictionary<int, CharaModel>();
            // var count = jsonDatas.Count;
            // for (var i = 0; i < count; i++)
            // {
            //     var data = jsonDatas[i];
            //     var chara = new CharaModel();
            //     chara.Id = (int)data["Id"];
            //     chara.Rarity = (int)data["Rarity"];
            //     chara.Name = (string)data["Name"];
            //     chara.Description = (string)data["Description"];
            //     chara.IconName = (string)data["IconName"];

            //     CharaModels.Add(chara.Id, chara);
            // }
        }
    }
}
