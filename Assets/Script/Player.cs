using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    //インスペクターで設定可能にする
    [Header("移動速度")] public float speed;
    [Header("重力")] public float gravity;
    [Header("ジャンプ速度")] public float jumpSpeed;
    [Header("ジャンプ高度")] public float jumpHeight;
    [Header("ジャンプ制限時間")] public float jumpLimitTime;
    [Header("踏み付け判定の高さの割合")] public float stepOnRate;
    [Header("接地判定")] public GroundCheck ground;
    [Header("頭ぶつけた判定")] public GroundCheck head;

    //プライベート変数
    private Animator anim = null;   
    private Rigidbody2D rb = null;
    private CapsuleCollider2D capsule = null;
    private bool isGround = false;
    private bool isHead = false;
    private bool isJump = false;
    private bool isRun = false;
    private bool isHit = false;
    private bool isOtherJump = false;
    private float jumpPos = 0.0f;
    private float otherJumpHeight = 0.0f;
    private float jumpTime = 0.0f;
  
    private string enemyTag = "Enemy";
    // Start is called before the first frame update
    void Start()
    {
        //コンポーネントのインスタンスを取得
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isHit)
        {
            //接地判定を得る
            isGround = ground.IsGround();
            isHead = head.IsGround();

            //各種座標の速度を求める   
            float xSpeed = GetXSpeed();
            float ySpeed = GetYSpeed();

            //アニメーションを適用
            SetAnimation();

            //移動速度を設定   
            rb.velocity = new Vector2(xSpeed, ySpeed);
        }
        else
        {
            rb.velocity = new Vector2(0, -gravity);
        }
    }

    private float GetXSpeed()
    {
        float horizontalKey = Input.GetAxis("Horizontal");
        float xSpeed = 0.0f;//移動速度

        //移動時のみrunにする
        if (horizontalKey > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);//右方向
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
        return xSpeed;
    }

    private float GetYSpeed()
    {
        float verticalKey = Input.GetAxis("Vertical");
        float ySpeed = -gravity;
        if (isOtherJump)
        {
            //上方向キーを押しているか
            bool pushUpKey = verticalKey > 0;
            //現在の高さが飛べる高さより下か
            bool canHeight = jumpPos + otherJumpHeight > transform.position.y;
            //ジャンプ時間が長くなりすぎていないか
            bool canTime = jumpLimitTime > jumpTime;

            if (canHeight && canTime && !isHead)
            {
                ySpeed = jumpSpeed;
                //上昇している間に進んだゲーム内時間を足す
                jumpTime += Time.deltaTime;//Time.deltaTimeはゲーム内時間を進める秒数
            }
            else
            {
                isOtherJump = false;
                jumpTime = 0.0f;
            }
        }

        else if (isGround)
        {
            if (verticalKey > 0)
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
        return ySpeed;
    }
    private void SetAnimation()
    {
        anim.SetBool("jump", isJump || isOtherJump);
        anim.SetBool("ground", isGround);
        anim.SetBool("run", isRun);
    }
    //ダウンアニメーションが完了しているかどうか
    private bool IsHitAnimEnd()
    {
        if(isHit && anim != null)
        {
            AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
        }
        return false;
    }
    //接触判定
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == enemyTag)
        {
            //踏み付け判定になる高さ
            float stepOnHeight = (capsule.size.y * (stepOnRate / 100f));

            //踏み付け判定のワールド座標
            float judgePos = transform.position.y - (capsule.size.y / 2f) + stepOnHeight;
            foreach (ContactPoint2D p in collision.contacts)
            { 
                if(p.point.y < judgePos)//衝突した位置
                {
                    //もう一度跳ねる
                    ObjectCollision o = collision.gameObject.GetComponent<ObjectCollision>(); 
                    if(o != null)
                    {
                        otherJumpHeight = o.boundHeight;//踏んづけたものから跳ねる高さを取得する
                        o.playerStepOn = true;//踏んづけたものに対して踏んづけたことを通知する
                        jumpPos = transform.position.y;//ジャンプした位置を記録する
                        isOtherJump = true;
                        isJump = false;
                        jumpTime = 0.0f;
                    }
                    
                }
                else
                {
                    //ダウンする
                    anim.Play("New Animation_hit");
                    isHit = true;
                    break;
                }
            }
            Debug.Log("敵と接触した");
        }
    }
}
