using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    private Animator b_anim;    // 爆風アニメ

	// Use this for initialization
	void Start () {
        b_anim = GetComponent<Animator>();	
	}
	
	// Update is called once per frame
	void Update () {
        // 爆風アニメが設定されている場合
		if( b_anim.GetCurrentAnimatorStateInfo(0).IsName("bomb"))
        {
            // アニメが終了している場合
            if(b_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                // 爆風オブジェクト破棄
                Destroy(gameObject);
            }
        }
	}
}
