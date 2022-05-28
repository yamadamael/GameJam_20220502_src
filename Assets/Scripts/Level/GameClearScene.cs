using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace gamejam
{
    public class GameClearScene : MonoBehaviour
    {
        [SerializeField]
        Text _text;

        [SerializeField]
        Button _buttonTitle;
        [SerializeField]
        Button _buttonTweet;
        [SerializeField]
        Button _buttonRanking;

        public static GameModel GameModel;

        void Awake()
        {
            App.FadeManager.FadeIn(1);
        }

        void Start()
        {
            //_text.text = string.Format(_text.text, (HomeScene.GameModel.Date - HomeScene.GameModel.StartDate).Days);

            _buttonTitle.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(SceneConst.Scenes.Title.ToString(), 0);
            });

            _buttonTweet.onClick.AddListener(() =>
            {
                var score = 0;
                OnClickTweetButton(score);
            });

            _buttonRanking.onClick.AddListener(() =>
            {
                var score = 0;
                OnClickScoreButton(score);
            });
        }

        private void OnClickTweetButton(int score)
        {
            var text = $"{score}日で9股を達成！みんなで楽しく学園生活を楽しもうね♪";
            // TODO
            var url = "love9";
            var tags = new string[] { "unity1week" };
            naichilab.UnityRoomTweet.Tweet(url, text, tags);
        }

        private void OnClickScoreButton(int score)
        {
            // ニフクラはasurasurasuraアカウント
            // https://naichilab.hatenablog.com/entry/webgl-simple-ranking
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(score);
            naichilab.RankingSceneManager.IsOpened = true;
        }
    }
}
