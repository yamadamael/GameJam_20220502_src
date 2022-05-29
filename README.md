# 概要
[はばたけ野菜少女](https://unityroom.com/games/flyingyasaigarl)

unity1week 「そろえる」に参加した際に作成したゲームのソースコードとゲームデータです。<br>
Unityから開いて[使用したアセット/ライブラリ](https://github.com/yamadamael/GameJam_20220502_src/edit/main/README.md#%E4%BD%BF%E7%94%A8%E3%82%A2%E3%82%BB%E3%83%83%E3%83%88%E3%83%A9%E3%82%A4%E3%83%96%E3%83%A9%E3%83%AA)を導入しBoot.unityから再生すれば動作する想定です。<br>
音声やキャラ画像はmetaデータのみコミットしています。<br>

### Assets/Scripts/App/
unity1weekで使いまわしているテンプレート的なものです。

### Assets/Scripts/Level/
各シーンごとのソースです。

### Assets/Scripts/Model/
jsonファイルから取り込んだデータやゲーム中に保存するデータなどを扱っています。

### Assets/Resources/GameData/
ゲームで使う決まった値を定義したjsonファイルです。

# 使用アセット/ライブラリ
- [DOTween](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676?locale=ja-JP)
- [LitJSON](https://litjson.net/)
- [unity-simple-ranking](https://github.com/naichilab/unity-simple-ranking)
- [unityroom-tweet](https://github.com/naichilab/unityroom-tweet)
- [NotoSansCJKjp](https://fonts.google.com/noto/specimen/Noto+Sans+JP)
