using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    //インスペクターで設定可能にする
    public float speed;
    //プライベート変数
    private Animator amim = null;
    private Rigidbody2D rb = null;
    // Start is called before the first frame update
    void Start()
    {
        //コンポーネントのインスタンスを取得
        amim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalKey = Input.GetAxis("Horizontal");//水平方向の読み取り
        float xSpeed = 0.0f;//移動速度

        //移動時のみrunにする
        if (horizontalKey > 0)
        {
            transform.localScale = new Vector3(1,1,1);//右方向
            amim.SetBool("run", true);
            xSpeed = speed;
        }
        else if (horizontalKey < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);//左方向
            amim.SetBool("run", true);
            xSpeed = -speed;
        }
        else
        {
            amim.SetBool("run", false);//何も押してないとき
            xSpeed = 0.0f;
        }
        rb.velocity = new Vector2(xSpeed,rb.velocity.y);
    }
}
