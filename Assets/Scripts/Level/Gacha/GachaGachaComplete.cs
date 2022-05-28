using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace gamejam
{
    public class GachaGachaComplete : MonoBehaviour
    {
        [SerializeField]
        CharaIcon _compReward;
        [SerializeField]
        CharaIcon[] _compTargets;
        [SerializeField]
        Button _completeButton;
        [SerializeField]
        Text _text;

        public void Init()
        {
            var getList = App.UserDataManager.Data.GetList;

            var complete = App.ModelManager.Gacha.Complete;
            _compReward.Init(0, complete.CompReward, false, null);
            _compReward.SetGrayFilter(!getList.Contains(complete.CompReward));
            var count = _compTargets.Length;
            for (var i = 0; i < count; i++)
            {
                _compTargets[i].Init(0, complete.CompTargets[i], false, null);
                _compTargets[i].SetGrayFilter(!getList.Contains(complete.CompTargets[i]));
                if (getList.Contains(complete.CompTargets[i]))
                {
                    _compTargets[i].SetBeatAnimation();
                }
            }

            var isComp = true;
            foreach (var target in App.ModelManager.Gacha.Complete.CompTargets)
            {
                if (!App.UserDataManager.Data.IsExistGetList(target))
                {
                    isComp = false;
                    break;
                }
            }
            _completeButton.interactable = isComp;

            _completeButton.onClick.AddListener(OnClickCompButton);
        }

        public void OnClickCompButton()
        {
            var getList = App.UserDataManager.Data.GetList;
            var complete = App.ModelManager.Gacha.Complete;
            foreach (var target in complete.CompTargets)
            {
                if (!getList.Contains(target))
                {
                    // TODO システムメッセ―ジ
                    Debug.Log("あなたはまだコンプしてません。");
                    return;
                }
            }

            if (App.UserDataManager.Data.IsExistGetList(complete.CompReward))
            {
                // 獲得済み
            }
            else
            {
                // 獲得処理
                App.UserDataManager.UserChara.AddUserChara(complete.CompReward);
            }

            App.AudioManager.PlayBGM("Sound/BGM_Comp", true);
            SceneManager.LoadSceneAsync(SceneConst.Scenes.Comp.ToString(), 0.3f, null);
        }

        public void KillSequence()
        {
            foreach (var target in _compTargets)
            {
                target.KillSequence();
            }
        }
    }
}
