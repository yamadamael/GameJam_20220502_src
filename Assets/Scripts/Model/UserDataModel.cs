using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gamejam
{
    public class UserDataModel
    {
        public int Money;
        public int ClearMoney;
        public int Gem;

        // 獲得済みキャラIDリスト
        public List<int> GetList;

        public UserDataModel()
        {
            GetList = new List<int>();
        }

        public void AddGetList(int charaId)
        {
            if (GetList.Contains(charaId))
            {
                return;
            }

            GetList.Add(charaId);
            GetList.Sort();
        }

        public bool IsExistGetList(int charaId)
        {
            return GetList.Contains(charaId);
        }
    }
}
