using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gamejam
{
    public class GachaModel
    {
        // レアリティ, 1000分率
        public Dictionary<int, GachaRarityRateModel> RarityRateList;
        public GachaCompleteModel Complete;

        public GachaModel()
        {
            RarityRateList = new Dictionary<int, GachaRarityRateModel>();
            Complete = new GachaCompleteModel();
        }

        public void Init()
        {
            {
                var path = "GameData/GachaRarityRate";
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
                    var rarityRateModel = new GachaRarityRateModel();
                    rarityRateModel.Rarity = (int)data["Rarity"];
                    rarityRateModel.Rate = (float)System.Convert.ToDouble((string)data["Rate"]);
                    RarityRateList.Add(rarityRateModel.Rarity, rarityRateModel);
                }
            }

            {
                var path = "GameData/GachaCharaRate";
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
                    var charaRateModel = new GachaCharaRateModel();
                    charaRateModel.CharaId = (int)data["CharaId"];
                    charaRateModel.Magnification = (float)System.Convert.ToDouble((string)data["Magnification"]);
                    var charaModel = App.ModelManager.CharaModels[charaRateModel.CharaId];
                    var rarityRateModel = RarityRateList[charaModel.Rarity];
                    rarityRateModel.CharaRateList.Add(charaRateModel);
                }
            }

            {
                var path = "GameData/GachaComplete";
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
                    if (0 < (int)data["CompReward"])
                    {
                        Complete.CompReward = (int)data["CharaId"];
                    }
                    else
                    {
                        Complete.CompTargets.Add((int)data["CharaId"]);
                    }
                }
            }
        }

        public List<GachaResultInfo> DrawGacha(int count)
        {
            var charaIds = new List<int>();

            for (var i = 0; i < count; i++)
            {
                var isHit = false;
                var rand = Random.Range(0f, 1000f);
                var accum = 0f;
                foreach (var rarityRateModel in RarityRateList.Values)
                {
                    if (accum + rarityRateModel.Rate < rand)
                    {
                        accum += rarityRateModel.Rate;
                        continue;
                    }

                    var charaAccum = 0f;
                    var charaSum = rarityRateModel.CharaRateList.Sum(x => x.Magnification);
                    foreach (var charaRateModel in rarityRateModel.CharaRateList)
                    {
                        charaAccum += charaRateModel.Magnification;
                        var a = (rand - accum) / rarityRateModel.Rate;
                        var b = charaAccum / charaSum;
                        if (a <= b)
                        {
                            charaIds.Add(charaRateModel.CharaId);
                            isHit = true;
                            break;
                        }
                    }

                    break;
                }

                if (!isHit)
                {
                    Debug.Log(string.Format("ガチャ抽選数が合ってない, {0}, {1}", rand, accum));
                    var last = RarityRateList[Define.RarityR].CharaRateList.Last();
                    charaIds.Add(last.CharaId);
                }
            }

            var resultInfos = new List<GachaResultInfo>();
            foreach (var charaId in charaIds)
            {
                // キャラ追加より先に判定する
                var isNew = !App.UserDataManager.Data.IsExistGetList(charaId);

                // ユーザーキャラ追加
                var userId = App.UserDataManager.UserChara.AddUserChara(charaId);

                resultInfos.Add(new GachaResultInfo
                {
                    CharaId = charaId,
                    UserId = userId,
                    IsNew = isNew,
                });
            }

            if (App.UserDataManager.Data.ClearMoney == 0)
            {
                var isComp = true;
                foreach (var target in App.ModelManager.Gacha.Complete.CompTargets)
                {
                    if (!App.UserDataManager.Data.IsExistGetList(target))
                    {
                        isComp = false;
                        break;
                    }
                }
                if (isComp)
                {
                    // コンプ時のお金保存
                    App.UserDataManager.Data.ClearMoney = App.UserDataManager.Data.Money;
                }
            }


            GachaLog(resultInfos);

            // お金加算
            App.UserDataManager.Data.Money += Define.CharaPerMoney * count;

            return resultInfos;
        }

        void GachaLog(List<GachaResultInfo> resultInfos)
        {
            var dic = new Dictionary<int, int>();
            foreach (var resultInfo in resultInfos)
            {
                var userChara = App.UserDataManager.UserChara.GetUserChara(resultInfo.UserId);
                if (userChara == null)
                {
                    continue;
                }
                var charaId = userChara.Id;
                if (dic.ContainsKey(charaId))
                {
                    dic[charaId]++;
                }
                else
                {
                    dic.Add(charaId, 1);
                }
            }

            var keys = dic.Keys.ToList();
            keys.Sort();
            var s = "";
            for (var i = 0; i < keys.Count; i++)
            {
                var key = keys[i];
                s += (string.Format("{0}, {1}\n", key, dic[key]));

                // if (i == keys.Count / 2)
                // {
                //     Debug.Log(s);
                //     s = "";
                // }
            }

            Debug.Log(s);
        }

    }
}
