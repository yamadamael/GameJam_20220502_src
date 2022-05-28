using System.Collections.Generic;
using UnityEngine;

namespace gamejam
{
    public class SceneManager : MonoBehaviour
    {
        private static BaseScene _currentScene;
        public static BaseScene CurrentScene { get { return _currentScene; } }

        public static void LoadScene(string sceneName, float time)
        {
            App.FadeManager.FadeOut(
                time,
                () => UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName));
        }

        public static void LoadSceneAsync(string sceneName, float time, Dictionary<string, object> paramList = null)
        {
            App.FadeManager.FadeOut(
                time,
                () =>
                {
                    var scene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
                    scene.completed += (x) =>
                    {
                        var scene = FindObjectOfType<BaseScene>() as BaseScene;
                        SetSceneParams(scene, paramList);

                        _currentScene = scene;
                    };
                });
        }

        private static void SetSceneParams(BaseScene scene, Dictionary<string, object> paramList)
        {
            scene.ParamMap = paramList;
        }
    }
}
