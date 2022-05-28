using UnityEngine;
using UnityEngine.UI;

namespace gamejam
{
    public class TitleScene : MonoBehaviour
    {
        [SerializeField]
        private Button _startButton = null;
        [SerializeField]
        private Config _config;

        void Awake()
        {
            _startButton.onClick.AddListener(OnClickStartButton);
            _config.Init();

            App.FadeManager.FadeIn(1);
            App.AudioManager.PlayBGM("Sound/BGM_Theme", true, 0.75f);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                OnClickStartButton();
            }

            _config.UpdateConfig();
        }

        private void OnClickStartButton()
        {
            SceneManager.LoadSceneAsync(SceneConst.Scenes.Gacha.ToString(), 0.3f);
        }
    }
}
