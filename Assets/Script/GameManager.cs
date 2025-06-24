using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header("現在のステージ")] public int stageNum;
    [Header("現在の残機")] public int heartNum;
    [Header("現在の復帰位置")] public int continueNum;
    [Header("デフォルトの残機")] public int defaultHeartNum;

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

        // タイトル画面に戻ったら状態初期化
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            ResetGameState();
        }
    }

    // 残機を減らす
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

    // タイトルに戻るとき状態リセット
    public void ResetGameState()
    {
        isGameOver = false;
        isStageClear = false;
        heartNum = defaultHeartNum;
        continueNum = 0;
        stageNum = 1;
    }
}
