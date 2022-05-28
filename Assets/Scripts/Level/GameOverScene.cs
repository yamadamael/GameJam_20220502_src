using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace gamejam
{
    public class GameOverScene : MonoBehaviour
    {
        [SerializeField]
        Text _text;

        [SerializeField]
        Button _button;

        public static GameModel GameModel;

        void Awake()
        {
            App.FadeManager.FadeIn(1);
        }

        // Start is called before the first frame update
        void Start()
        {
            _button.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(SceneConst.Scenes.Title.ToString(), 0);
            });
        }
    }
}
