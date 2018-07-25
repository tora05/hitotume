using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour {

    public float scrollSpeed = 0.5f; // スクロールの速度

    public Renderer rend;   // 描画管理システム

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update () {
        // 背景のスクロールをループして管理
        float scroll = Mathf.Repeat(Time.time * scrollSpeed, 1);
        // Ｙ方向のスクロール
        Vector2 offset = new Vector2(0, scroll);
        // シェーダー側のテクスチャのUV位置をずらしてスクロール
        rend.material.SetTextureOffset("_MainTex", offset);
	}
}
