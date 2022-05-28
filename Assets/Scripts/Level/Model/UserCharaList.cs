using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gamejam
{
    public class UserCharaList
    {
        public Dictionary<string, UserCharaModel> List;

        public UserCharaList()
        {
            List = new Dictionary<string, UserCharaModel>();
        }

        public string AddUserChara(int charaId)
        {
            var userCharaModel = new UserCharaModel();
            var guid = Guid.NewGuid();
            var userId = guid.ToString();
            userCharaModel.Id = charaId;
            userCharaModel.UserId = userId;
            var dto = new DateTimeOffset(DateTime.Now.Ticks, new TimeSpan(+09, 00, 00));
            userCharaModel.Date = dto.ToUnixTimeSeconds();

            List.Add(userId, userCharaModel);

            // 図鑑更新
            App.UserDataManager.Data.AddGetList(charaId);

            return userId;
        }

        public void RemoveUserChara(long userId)
        {
            // TODO
        }

        public UserCharaModel GetUserChara(string userId)
        {
            if (!List.ContainsKey(userId))
            {
                Debug.Log("ユーザーキャラなし");
                return null;
            }
            return List[userId];
        }
    }
}
