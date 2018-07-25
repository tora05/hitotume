using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public int speed = 10;      // 弾の速度

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // 加速度を上方向矢印(緑)に向かってかける
        GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speed;
        
        // 画面外に出たら消す
        if( transform.position.y > 1.5f)
        {
            Destroy(gameObject);
        }
	}
}
