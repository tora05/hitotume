//==============================================================
//  概要：アスペクト比固定管理
//        iOS側が2018年4月1日からiPhone10の上下の枠に収まるようにしないと
//        審査に通らなくなったため必須処理となります。
//  制作者:Y.Saito
//  制作日:18/05/23
//
//==============================================================
//====================================================
//  ダイナミックリンクライブラリ(dll)の参照呼び出し
//====================================================
using UnityEngine;  // Unity標準ライブラリ呼び出し
using System.Collections; // 

/// <summary>
/// スクリプトクラスの宣言
/// </summary>
[ExecuteInEditMode] // Editor上でスクリプトが実行されるようにする
public class AspectCamera : MonoBehaviour {

    //=========================================
    // 変数宣言
    //=========================================
    // アスペクト比固定
    public Vector2 aspect = new Vector2(4, 3);
    // 表示領域外の背景色(アスペクト比固定した場合)
    public Color32 backgroundColor = Color.black;

    private float aspectRate;   // 画面比率固定
    // オブジェクトの場合は変数名の前に_(アンダーバー)
    // を付けるとわかりやすい(忘れがち)
    private Camera _camera; // 表示領域カメラ
    private static Camera _backgroundCamera; // 表示領域外カメラ

    //==========================================
    //  関数宣言
    //==========================================
	// アクティブ時に一度だけ実行
	void Start () {
        // アスペクト比を設定
        aspectRate = (float)aspect.x / aspect.y;
        // メインカメラを呼び出し
        _camera = GetComponent<Camera>();
        // 背景のカメラを作成
        CreateBackgroundCamera();
        // 
        UpdateScreenRate();
        // アスペクト比固定を使用するかどうか
        //enabled = false;
    }

    /// <summary>
    /// 背景カメラの作成
    /// </summary>
    void CreateBackgroundCamera()
    {
#if UNITY_EDITOR
        if (!UnityEditor.EditorApplication.isPlaying)
            return;
#endif
        // 背景カメラが作成済みなら処理終了
        if (_backgroundCamera != null)
            return;

        // 背景カメラ作成(オブジェクト名はBackground Color Camera)
        var backGroundCameraObject = new GameObject("Background Color Camera");
        // 作成したカメラをこの関数以外でも呼び出しできるようにする
        _backgroundCamera = backGroundCameraObject.AddComponent<Camera>();
        _backgroundCamera.depth = -99;          // 一番後ろに表示
        _backgroundCamera.fieldOfView = 0;      // カメラの視野設定
        _backgroundCamera.farClipPlane = 1.1f;  // カメラに映る奥側平面距離
        _backgroundCamera.nearClipPlane = 1;    // カメラに映る手前側平面距離
        _backgroundCamera.cullingMask = 0;      // カメラマスク処理無効
        _backgroundCamera.depthTextureMode = DepthTextureMode.None; // 深度テクスチャモード無効
        _backgroundCamera.backgroundColor = backgroundColor;    // 表示領域外の背景色設定
        // VertexLit は VertexLightingの略　頂点ライティングの設定
        _backgroundCamera.renderingPath = RenderingPath.VertexLit; // レンダリングパイプラインの設定(ライティング方法や影のレンダリング方法の設定)
        // SolidColorは単色
        _backgroundCamera.clearFlags = CameraClearFlags.SolidColor; // 画面のクリア方法
        _backgroundCamera.useOcclusionCulling = false;     // カメラに映らないものを描画領域に載せない設定(falseはこの設定を使用しないので表示される)
        backGroundCameraObject.hideFlags = HideFlags.NotEditable;   // オブジェクトの情報をインスペクタで編集不可にする
    }

    // アスペクト比固定の更新
    void UpdateScreenRate()
    {
        float baseAspect = aspect.y / aspect.x; // 設定する画面比率(アスペクト)
        // 今現在の画面のアスペクト
        float nowAspect = (float)Screen.height / Screen.width;

        // 画面よりも設定するアスペクト比固定のサイズが大きいなら
        if (baseAspect > nowAspect)
        {
            // 表示領域外と表示領域の比率を取得
            var changeAspect = nowAspect / baseAspect;
            // 横方向を整える
            _camera.rect = new Rect((1 - changeAspect) * 0.5f, 0, changeAspect, 1);
        }
        else
        {
            // 表示領域外と表示領域の比率を取得
            var changeAspect = baseAspect / nowAspect;
            // 縦方向を整える
            _camera.rect = new Rect(0, (1 - changeAspect) * 0.5f, 1, changeAspect);
        }
    }

    /// <summary>
    /// 画面比率が変更されていたら処理する
    /// </summary>
    /// <returns>変更されていたら偽、変更がなければ真</returns>
    bool IsChangeAspect()
    {
        return _camera.aspect == aspectRate;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // 画面比率に変更があれば終了せずにアスペクト比固定処理をもう一度実行
        if (IsChangeAspect())
            return;

        // もう一度アスペクト比固定をする
        UpdateScreenRate();
        _camera.ResetAspect();  // カメラの方の画面比率を設定しなおし
    }
}