using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace gamejam
{
    public class GachaChara : MonoBehaviour
    {
        [SerializeField]
        GameObject _charaObject;
        [SerializeField]
        CharaIcon _charaIcon;
        [SerializeField]
        Button _rightButton;
        [SerializeField]
        Button _leftButton;
        [SerializeField]
        Text _commentText;
        [SerializeField]
        Text _appealText;

        int _index;
        List<int> _getList;

        public void Init()
        {
            _getList = App.UserDataManager.Data.GetList;
            SetCharaIcon();

            _charaObject.gameObject.SetActive(_getList.Count != 0);
            _appealText.gameObject.SetActive(_getList.Count == 0);

            _leftButton.onClick.AddListener(OnClickLeftButton);
            _rightButton.onClick.AddListener(OnClickRightButton);
        }

        public void SetCharaIcon()
        {
            if (_getList.Count == 0)
            {
                return;
            }

            var charaId = _getList[_index];
            _charaIcon.Init(0, charaId, false, null);
            var chara = App.ModelManager.CharaModels[charaId];
            _commentText.text = chara.Name;
        }

        public void OnClickLeftButton()
        {
            _index--;
            if (_index < 0)
            {
                _index = _getList.Count - 1;
            }
            SetCharaIcon();
            App.AudioManager.PlaySE("Sound/shultu");
        }

        public void OnClickRightButton()
        {
            _index++;
            if (_getList.Count <= _index)
            {
                _index = 0;
            }
            SetCharaIcon();
            App.AudioManager.PlaySE("Sound/shultu");
        }
    }
}
