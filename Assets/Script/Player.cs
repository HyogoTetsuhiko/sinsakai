using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    //�C���X�y�N�^�[�Őݒ�\�ɂ���
    public float speed;
    //�v���C�x�[�g�ϐ�
    private Animator amim = null;
    private Rigidbody2D rb = null;
    // Start is called before the first frame update
    void Start()
    {
        //�R���|�[�l���g�̃C���X�^���X���擾
        amim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalKey = Input.GetAxis("Horizontal");//���������̓ǂݎ��
        float xSpeed = 0.0f;//�ړ����x

        //�ړ����̂�run�ɂ���
        if (horizontalKey > 0)
        {
            transform.localScale = new Vector3(1,1,1);//�E����
            amim.SetBool("run", true);
            xSpeed = speed;
        }
        else if (horizontalKey < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);//������
            amim.SetBool("run", true);
            xSpeed = -speed;
        }
        else
        {
            amim.SetBool("run", false);//���������ĂȂ��Ƃ�
            xSpeed = 0.0f;
        }
        rb.velocity = new Vector2(xSpeed,rb.velocity.y);
    }
}
