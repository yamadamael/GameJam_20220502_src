using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaResultInfo
{
    public int CharaId;
    public string UserId;
    public bool IsNew;
}

namespace gamejam
{
    public class GachaResult : MonoBehaviour
    {
        [SerializeField]
        CharaIcon[] _charaIcons;
        [SerializeField]
        GameObject _uiParent;
        [SerializeField]
        Button _backButton;
        [SerializeField]
        GameObject _compObject;
        [SerializeField]
        GameObject _fullCompObject;
        [SerializeField]
        Button _gacha1Button;
        [SerializeField]
        Button _gacha2Button;
        [SerializeField]
        Text _money;

        public void Init(List<GachaResultInfo> resultInfos)
        {
            var count = _charaIcons.Length;
            for (var i = 0; i < count; i++)
            {
                var charaIcon = _charaIcons[i];

                if (i < resultInfos.Count)
                {
                    var resultInfo = resultInfos[i];
                    charaIcon.Init(i, resultInfo.CharaId, resultInfo.IsNew, OnFinishShowIcon);
                }

                charaIcon.gameObject.SetActive(false);
            }

            _uiParent.gameObject.SetActive(false);
            _backButton.onClick.AddListener(OnClickBackButton);
            var isFullComp = App.UserDataManager.Data.GetList.Count == App.ModelManager.CharaModels.Count;
            var compReward = App.ModelManager.Gacha.Complete.CompReward;
            var isExistCompReward = App.UserDataManager.Data.IsExistGetList(compReward);
            var compTargets = App.ModelManager.Gacha.Complete.CompTargets;
            var isExistCompTargets = compTargets.All(x => App.UserDataManager.Data.IsExistGetList(x));
            _fullCompObject.SetActive(isFullComp && !(!isExistCompReward && isExistCompTargets));
            _compObject.SetActive(!isFullComp && !isExistCompReward && isExistCompTargets);
            _gacha1Button.onClick.AddListener(() => OnClickGachaButton(1));
            _gacha2Button.onClick.AddListener(() => OnClickGachaButton(10));
            _money.text = string.Format("{0:#,0}", App.UserDataManager.Data.Money);
        }

        void OnClickBackButton()
        {
            SceneManager.LoadSceneAsync(SceneConst.Scenes.Gacha.ToString(), 0.3f);
        }

        void OnClickGachaButton(int count)
        {
            var resultInfos = App.ModelManager.Gacha.DrawGacha(count);

            var paramList = new Dictionary<string, object>();
            paramList.Add("resultInfos", resultInfos);
            SceneManager.LoadSceneAsync(SceneConst.Scenes.GachaResult.ToString(), 0.3f, paramList);
        }

        void OnFinishShowIcon()
        {
            foreach (var icon in _charaIcons)
            {
                if (0 < icon.CharaId && !icon.IsFinishEffect())
                {
                    return;
                }
            }

            // ゲーム自体のタップ可能にしたほうがいいかも(アイコン押される)
            _uiParent.gameObject.SetActive(true);
        }

        public void Show()
        {
            gameObject.SetActive(true);

            var count = _charaIcons.Length;
            for (var i = 0; i < count; i++)
            {
                var charaIcon = _charaIcons[i];
                if (charaIcon.CharaId != 0)
                {
                    charaIcon.Show();
                }
            }
        }
    }
}
