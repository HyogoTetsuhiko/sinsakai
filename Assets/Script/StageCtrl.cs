using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCtrl : MonoBehaviour
{
    [Header("�v���C���[�Q�[���I�u�W�F�N�g")] public GameObject playerObj;
    [Header("�R���e�B�j���[�ʒu")] public GameObject[] continuePoint;

    private Player p;
    void Start()
    {
        if(playerObj != null && continuePoint != null && continuePoint.Length > 0)
        {
            playerObj.transform.position = continuePoint[0].transform.position;

            p = playerObj.GetComponent<Player>();
            if( p == null )
            {
                Debug.Log("�v���C���[���ᖳ�����ɃA�^�b�`���Ă�");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(p != null && p.IsCountinueWaiting())
        {
            if (continuePoint.Length > GameManager.instance.continueNum)
            {
                playerObj.transform.position = continuePoint[GameManager.instance.continueNum].transform.position;
                p.ContinuePlayer();
            }
            else
            {
                Debug.Log("�R���e�B�j���[�|�C���g�̐ݒ肪����Ȃ�");
            }
        } 
    }
}
