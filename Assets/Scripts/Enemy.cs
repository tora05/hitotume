using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    private GameObject Bomb;    // 倒されたときの爆風

    int moveState = 0;      // 左右の移動状態
    float moveSpeed = 0;    // 移動速度

    float upDownMove = 0;   // 上下移動の角速度

    float RespornTime = 0;  // 再登場までの時間計測

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ChangeState();  // 左右移動の状態
        ChangeMove();   // 移動速度変更
        Move();         // 移動

        Resporn();      // 再登場
    }

    /// <summary>
    /// 再登場
    /// </summary>
    void Resporn()
    {
        // 一度打たれて表示されていなかったら
        if(!gameObject.GetComponent<SpriteRenderer>().enabled)
        {
            RespornTime += Time.deltaTime;  // 再登場までの時間計測

            // 再登場の時間になったら表示してまた再登場までの時間クリア
            if(RespornTime > 3.0f)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                RespornTime = 0.0f;
            }
        }
    }

    /// <summary>
    /// 左右移動の状態
    /// </summary>
    void ChangeState()
    {
        // 右に行き過ぎた場合左移動状態に変更
        if( transform.position.x > 0.6f)
        {
            moveState = 1;
        }
        // 左に行き過ぎた場合右移動状態に変更
        else if(transform.position.x < -0.6f)
        {
            moveState = 0;
        }
    }

    /// <summary>
    /// 移動速度変更
    /// </summary>
    void ChangeMove()
    {
        // 右移動状態
        if (moveState == 0)
        {
            moveSpeed = 0.5f * Time.deltaTime;  // 右へ移動
        }
        // 左移動状態
        else if (moveState == 1)
        {
            moveSpeed = -0.5f * Time.deltaTime; // 左へ移動
        }
        upDownMove += 0.1f; // 角速度加算
    }

    /// <summary>
    /// 移動
    /// </summary>
    void Move()
    {
        // 横移動は加速度で縦移動は角速度で
        transform.position += new Vector3(moveSpeed, Mathf.Sin(Mathf.Repeat(upDownMove, 6.4f)) * Time.deltaTime, 0.0f);
    }

    /// <summary>
    /// 衝突判定
    /// </summary>
    /// <param name="collision">衝突したオブジェクトの判定</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 弾と衝突したら
        if (collision.gameObject.name.StartsWith("tama"))
        {
            // 表示されていたら
            if (gameObject.GetComponent<SpriteRenderer>().enabled)
            {
                // 爆風を起こして
                Instantiate(Bomb, transform.position, transform.rotation);
                // 非表示にする
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}
