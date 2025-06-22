using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    //インスペクターで設定可能にする
    public float speed;//速度
    public float gravity;//重力
    public float jumpSpeed;//ジャンプ速度
    public float jumpHeight;//ジャンプ高度
    public float jumpLimitTime;//ジャンプ制限時間
    public GroundCheck ground;//接地判定
    public GroundCheck head;//頭ぶつけた判定

    //プライベート変数
    private Animator anim = null;   
    private Rigidbody2D rb = null;
    private bool isGround = false;
    private bool isHead = false;
    private bool isJump = false;
    private float jumpPos = 0.0f;
    private float jumpTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        //コンポーネントのインスタンスを取得
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //接地判定を得る
        isGround = ground.IsGround();
        isHead = head.IsGround();
        //キー入力されたら行動する
        float horizontalKey = Input.GetAxis("Horizontal");
        float verticalKey = Input.GetAxis("Vertical");
        float xSpeed = 0.0f;//移動速度
        float ySpeed = -gravity ;

        if (isGround)
        {
            if(verticalKey > 0)
            {
                ySpeed = jumpSpeed;
                jumpPos = transform.position.y;//ジャンプした位置を記録
                isJump = true;
                jumpTime = 0.0f;
            }
            else
            {
                isJump = false;
            }
        }
        else if (isJump)
        {
            //上方向キーを押しているか
            bool pushUpKey = verticalKey > 0;
            //現在の高さが飛べる高さより下か
            bool canHeight = jumpPos + jumpHeight > transform.position.y;
            //ジャンプ時間が長くなりすぎていないか
            bool canTime = jumpLimitTime > jumpTime;
            if (pushUpKey && canHeight && canTime && !isHead)
            {
                ySpeed = jumpSpeed;
                //上昇している間に進んだゲーム内時間を足す
                jumpTime += Time.deltaTime;//Time.deltaTimeはゲーム内時間を進める秒数
            }
            else
            {
                isJump = false;
                jumpTime = 0.0f;
            }
        }
        //移動時のみrunにする
        if (horizontalKey > 0)
        {
            transform.localScale = new Vector3(1,1,1);//右方向
            anim.SetBool("run", true);
            xSpeed = speed;
        }
        else if (horizontalKey < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);//左方向
            anim.SetBool("run", true);
            xSpeed = -speed;
        }
        else
        {
            anim.SetBool("run", false);//何も押してないとき
            xSpeed = 0.0f;
        }
        anim.SetBool("jump", isJump);
        anim.SetBool("ground",isGround);
        rb.velocity = new Vector2(xSpeed,ySpeed);
    }
}
