using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCtrl : MonoBehaviour
{
    [Header("�v���C���[�Q�[���I�u�W�F�N�g")] public GameObject playerObj;
    [Header("�R���e�B�j���[�ʒu")] public GameObject[] continuePoint;
    [Header("�Q�[���I�[�o�[")] public GameObject gameOverObj;
    [Header("�X�e�[�W�N���A")]public GameObject StageClearObj;
    [Header("�X�e�[�W�N���A����")]public PlayerTriggerCheck stageClearTrigger;
     
    private Player p;
    private bool doGameOver = false;
    private bool retryGame = false;
    private bool doClear = false;
    void Start()
    {
        if(playerObj != null && continuePoint != null && continuePoint.Length > 0 && gameOverObj != null)
        {
            gameOverObj.SetActive(false);
            StageClearObj.SetActive(false);
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
        //  �Q�[���I�[�o�[���̏���
        if (GameManager.instance.isGameOver && !doGameOver)
        {
            gameOverObj.SetActive(true);
            doGameOver = true;
        }
        //�v���C���[�����ꂽ���̏���
        else if (p != null && p.IsCountinueWaiting() && !doGameOver)
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
        else if(stageClearTrigger != null && stageClearTrigger.isOn && !doGameOver && !doClear)
        {
            StageClear();
            doClear = true;
        }
    }
    public void Retry()
    {
        retryGame = true;
    }

    public void StageClear()
    {
        GameManager.instance.isStageClear = true;
        StageClearObj.SetActive(true);
    }
}
