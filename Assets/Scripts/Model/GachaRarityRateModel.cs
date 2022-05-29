using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gamejam
{
    public class GachaRarityRateModel
    {
        public int Rarity;
        // 1000分率
        public float Rate;

        public List<GachaCharaRateModel> CharaRateList;

        public GachaRarityRateModel()
        {
            CharaRateList = new List<GachaCharaRateModel>();
        }
    }
}
