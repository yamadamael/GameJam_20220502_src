using UnityEngine;

namespace gamejam
{
    public class App : MonoBehaviour
    {
        private Canvas _canvas;

        public static FadeManager FadeManager;
        public static AudioManager AudioManager;
        public static ModelManager ModelManager;
        public static UserDataManager UserDataManager;

        private static App _instance;
        public static App Instance
        {
            get
            {
                if (_instance == null)
                {
                    var appGo = new GameObject("App");
                    _instance = appGo.AddComponent<App>();
                    DontDestroyOnLoad(appGo);
                }
                return _instance;
            }
        }

        void Awake()
        {
            var canvasObj = Resources.Load<GameObject>("Prefab/App/Canvas");
            var canvasGo = Instantiate(canvasObj, this.transform);
            _canvas = canvasGo.GetComponent<Canvas>();

            var fadeManagerObj = Resources.Load<GameObject>("Prefab/App/FadeManager");
            var fadeManagerGo = Instantiate(fadeManagerObj, _canvas.transform);
            FadeManager = fadeManagerGo.GetComponent<FadeManager>();

            var audioManagerGo = new GameObject("AudioManager");
            audioManagerGo.transform.SetParent(transform);
            AudioManager = audioManagerGo.AddComponent<AudioManager>();

            ModelManager = new ModelManager();
            ModelManager.Init();

            UserDataManager = new UserDataManager();
            UserDataManager.Init();
        }
    }
}
