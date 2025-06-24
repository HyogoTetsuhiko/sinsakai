using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header("���݂̃X�e�[�W")] public int stageNum;
    [Header("���݂̎c�@")] public int heartNum;
    [Header("���݂̕��A�ʒu")] public int continueNum;
    [Header("�f�t�H���g�̎c�@")] public int defaultHeartNum;

    [HideInInspector] public bool isGameOver;
    [HideInInspector] public bool isStageClear;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            defaultHeartNum = 3;
        }
        else
        {
            Destroy(this.gameObject);
        }

        // �^�C�g����ʂɖ߂������ԏ�����
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            ResetGameState();
        }
    }

    // �c�@�����炷
    public void SubHeartNum()
    {
        if (heartNum > 0)
        {
            --heartNum;
        }
        else
        {
            isGameOver = true;
        }
    }

    // �^�C�g���ɖ߂�Ƃ���ԃ��Z�b�g
    public void ResetGameState()
    {
        isGameOver = false;
        isStageClear = false;
        heartNum = defaultHeartNum;
        continueNum = 0;
        stageNum = 1;
    }
}
