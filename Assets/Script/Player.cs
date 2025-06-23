using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    //�C���X�y�N�^�[�Őݒ�\�ɂ���
    [Header("�ړ����x")] public float speed;
    [Header("�d��")] public float gravity;
    [Header("�W�����v���x")] public float jumpSpeed;
    [Header("�W�����v���x")] public float jumpHeight;
    [Header("�W�����v��������")] public float jumpLimitTime;
    [Header("���ݕt������̍����̊���")] public float stepOnRate;
    [Header("�ڒn����")] public GroundCheck ground;
    [Header("���Ԃ�������")] public GroundCheck head;

    //�v���C�x�[�g�ϐ�
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
        //�R���|�[�l���g�̃C���X�^���X���擾
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isHit)
        {
            //�ڒn����𓾂�
            isGround = ground.IsGround();
            isHead = head.IsGround();

            //�e����W�̑��x�����߂�   
            float xSpeed = GetXSpeed();
            float ySpeed = GetYSpeed();

            //�A�j���[�V������K�p
            SetAnimation();

            //�ړ����x��ݒ�   
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
        float xSpeed = 0.0f;//�ړ����x

        //�ړ����̂�run�ɂ���
        if (horizontalKey > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);//�E����
            anim.SetBool("run", true);
            xSpeed = speed;
        }
        else if (horizontalKey < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);//������
            anim.SetBool("run", true);
            xSpeed = -speed;
        }
        else
        {
            anim.SetBool("run", false);//���������ĂȂ��Ƃ�
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
            //������L�[�������Ă��邩
            bool pushUpKey = verticalKey > 0;
            //���݂̍�������ׂ鍂����艺��
            bool canHeight = jumpPos + otherJumpHeight > transform.position.y;
            //�W�����v���Ԃ������Ȃ肷���Ă��Ȃ���
            bool canTime = jumpLimitTime > jumpTime;

            if (canHeight && canTime && !isHead)
            {
                ySpeed = jumpSpeed;
                //�㏸���Ă���Ԃɐi�񂾃Q�[�������Ԃ𑫂�
                jumpTime += Time.deltaTime;//Time.deltaTime�̓Q�[�������Ԃ�i�߂�b��
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
                jumpPos = transform.position.y;//�W�����v�����ʒu���L�^
               
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
            //������L�[�������Ă��邩
            bool pushUpKey = verticalKey > 0;
            //���݂̍�������ׂ鍂����艺��
            bool canHeight = jumpPos + jumpHeight > transform.position.y;
            //�W�����v���Ԃ������Ȃ肷���Ă��Ȃ���
            bool canTime = jumpLimitTime > jumpTime;
            if (pushUpKey && canHeight && canTime && !isHead)
            {
                ySpeed = jumpSpeed;
                //�㏸���Ă���Ԃɐi�񂾃Q�[�������Ԃ𑫂�
                jumpTime += Time.deltaTime;//Time.deltaTime�̓Q�[�������Ԃ�i�߂�b��
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
    //�_�E���A�j���[�V�������������Ă��邩�ǂ���
    private bool IsHitAnimEnd()
    {
        if(isHit && anim != null)
        {
            AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
        }
        return false;
    }
    //�ڐG����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == enemyTag)
        {
            //���ݕt������ɂȂ鍂��
            float stepOnHeight = (capsule.size.y * (stepOnRate / 100f));

            //���ݕt������̃��[���h���W
            float judgePos = transform.position.y - (capsule.size.y / 2f) + stepOnHeight;
            foreach (ContactPoint2D p in collision.contacts)
            { 
                if(p.point.y < judgePos)//�Փ˂����ʒu
                {
                    //������x���˂�
                    ObjectCollision o = collision.gameObject.GetComponent<ObjectCollision>(); 
                    if(o != null)
                    {
                        otherJumpHeight = o.boundHeight;//����Â������̂��璵�˂鍂�����擾����
                        o.playerStepOn = true;//����Â������̂ɑ΂��ē���Â������Ƃ�ʒm����
                        jumpPos = transform.position.y;//�W�����v�����ʒu���L�^����
                        isOtherJump = true;
                        isJump = false;
                        jumpTime = 0.0f;
                    }
                    
                }
                else
                {
                    //�_�E������
                    anim.Play("New Animation_hit");
                    isHit = true;
                    break;
                }
            }
            Debug.Log("�G�ƐڐG����");
        }
    }
}
