using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    //�C���X�y�N�^�[�Őݒ�\�ɂ���
    public float speed;//���x
    public float gravity;//�d��
    public float jumpSpeed;//�W�����v���x
    public float jumpHeight;//�W�����v���x
    public float jumpLimitTime;//�W�����v��������
    public GroundCheck ground;//�ڒn����
    public GroundCheck head;//���Ԃ�������

    //�v���C�x�[�g�ϐ�
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
        //�R���|�[�l���g�̃C���X�^���X���擾
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�ڒn����𓾂�
        isGround = ground.IsGround();
        isHead = head.IsGround();
        //�L�[���͂��ꂽ��s������
        float horizontalKey = Input.GetAxis("Horizontal");
        float verticalKey = Input.GetAxis("Vertical");
        float xSpeed = 0.0f;//�ړ����x
        float ySpeed = -gravity ;

        if (isGround)
        {
            if(verticalKey > 0)
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
        //�ړ����̂�run�ɂ���
        if (horizontalKey > 0)
        {
            transform.localScale = new Vector3(1,1,1);//�E����
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
        anim.SetBool("jump", isJump);
        anim.SetBool("ground",isGround);
        rb.velocity = new Vector2(xSpeed,ySpeed);
    }
}
