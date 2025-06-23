using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_01 : MonoBehaviour
{
    //�C���X�y�N�^�[�Őݒ肷��
    [Header("�ړ����x")] public float speed;
    [Header("�d��")] public float gravity;
    [Header("��ʊO�ł��s�����邩")] public bool nonVisible;
    [Header("�ڐG����")]public EnemyCollisionCheck checkCollision;

    //�v���C�x�[�g�ϐ�
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private ObjectCollision oc = null;
    private BoxCollider2D col = null;
    private bool rightTlefF = false;
    private bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        oc = GetComponent<ObjectCollision>();
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!oc.playerStepOn)
        {
            if (sr.isVisible || nonVisible)
            {
                if (checkCollision.isOn)
                {
                    rightTlefF = !rightTlefF;//���E���]
                }
                //�s������
                int xVector = -1;
                if (rightTlefF)
                {
                    xVector = 1;
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                rb.velocity = new Vector2(xVector * speed, -gravity);
            }
            else
            {
                rb.Sleep();
            }
        }
        //���܂ꂽ���̏���
        else
        {
            rb.velocity = new Vector2(0,-gravity);
            isDead = true;
            col.enabled = false;
            Destroy(gameObject, 3f);
        }
    }
}
