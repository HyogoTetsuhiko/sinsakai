using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCtrl : MonoBehaviour
{
    [Header("プレイヤーゲームオブジェクト")] public GameObject playerObj;
    [Header("コンティニュー位置")] public GameObject[] continuePoint;
    [Header("ゲームオーバー")] public GameObject gameOverObj;
     
    private Player p;
    private bool startFade = false;
    private bool doGameOver = false;
    private bool retryGame = false;
    private bool doSceneChange  = false;
    void Start()
    {
        if(playerObj != null && continuePoint != null && continuePoint.Length > 0 && gameOverObj != null)
        {
            gameOverObj.SetActive(false);
            playerObj.transform.position = continuePoint[0].transform.position;

            p = playerObj.GetComponent<Player>();
            if( p == null )
            {
                Debug.Log("プレイヤーじゃ無い物にアタッチしてる");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //  ゲームオーバー時の処理
        if (GameManager.instance.isGameOver && !doGameOver)
        {
            gameOverObj.SetActive(true);
            doGameOver = true;
        }
        //プレイヤーがやられた時の処理
        else if (p != null && p.IsCountinueWaiting() && !doGameOver)
        {
            if (continuePoint.Length > GameManager.instance.continueNum)
            {
                playerObj.transform.position = continuePoint[GameManager.instance.continueNum].transform.position;
                p.ContinuePlayer();
            }
            else
            {
                Debug.Log("コンティニューポイントの設定が足りない");
            }
        } 
    }
    public void Retry()
    {
        retryGame = true;
    }
}
