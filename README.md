# StreetViewVR
GearVRにGoogle Street Viewを表示するサンプルです。

###ビルド方法
- 実機で実行する場合は、取得したosigファイルをプロジェクト配下の
`Assets\Plugins\Android\assets`
に格納してください
[osigファイルの取得](https://developer.oculus.com/osig/)
 - DeviceIdの確認方法
   1. androidをPCに接続する
   2. コマンドプロンプトでadbのあるパスに移動（パスが通してあれば必要ない）
     `パス例　C:\Users\<ユーザ名>\AppData\Local\Android\sdk\platform-tools`
   3. 以下コマンドを実行
     `adb devices`

- File -> Build Settings -> Player Settingsの
 OtherSettingsのVirtual Reality Supportedにチェックを入れる

- Minimum API Levelを19にする

- android SDKパスを設定する（ビルド時に聞かれた場合）
例：C:\Users\<ユーザ名>\AppData\Local\Android\sdk
