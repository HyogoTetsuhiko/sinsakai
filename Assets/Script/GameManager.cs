using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [Header("���݂̎c�@")]public int heartNum;
    [Header("���݂̕��A�ʒu")]public int continueNum;
    [Header("�f�t�H���g�̎c�@")] public int defaultHeartNum;
    [HideInInspector] public bool isGameOver;
    [HideInInspector] public bool isStageClear;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    //�c�@�����炷
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
    public void RetryGame()
    {
        isGameOver = false;
        heartNum = defaultHeartNum;
        continueNum = 0;
    }

}
