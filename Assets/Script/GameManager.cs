using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [Header("現在の残機")]public int heartNum;
    [Header("現在の復帰位置")]public int continueNum;
    [Header("デフォルトの残機")] public int defaultHeartNum;
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
    //残機を減らす
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
