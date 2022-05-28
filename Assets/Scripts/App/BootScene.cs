using UnityEngine;

namespace gamejam
{
    public class BootScene : BaseScene
    {
        void Awake()
        {
            Screen.SetResolution(960, 540, false, 60);
            _ = App.Instance;
            App.FadeManager.FadeOut(0);
        }

        private void Start()
        {
            SceneManager.LoadScene(SceneConst.Scenes.Title.ToString(), 0);
        }
    }
}
