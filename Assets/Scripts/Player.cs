using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    float movex;    // Ｘ方向移動
    float movey;    // Ｙ方向移動

    [SerializeField]
    private GameObject Tama;    // 自機の弾

	// Use this for initialization
	void Start () {	
	}

    // Update is called once per frame
    void Update() {
        GetInput(); // キー入力
        Move(); // 移動
	}

    /// <summary>
    /// キー入力
    /// </summary>
    void GetInput()
    {
        movex = Input.GetAxis("Horizontal");    // X方向の入力を横軸速度に
        movey = Input.GetAxis("Vertical");      // Y方向の入力を縦軸速度に

        // 自機の弾を打ち出す
        if (Input.GetButtonDown("Jump"))
        {
            // 弾を生成
            Instantiate(Tama, transform.position, transform.rotation);
        }
    }

    /// <summary>
    /// 移動
    /// </summary>
    void Move()
    {
        // 自機の移動
        transform.Translate(movex * Time.deltaTime * 0.7f, movey * Time.deltaTime * 0.7f, 0.0f);
    }
}
