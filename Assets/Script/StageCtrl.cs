using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCtrl : MonoBehaviour
{
    [Header("プレイヤーゲームオブジェクト")] public GameObject playerObj;
    [Header("コンティニュー位置")] public GameObject[] continuePoint;
    [Header("ゲームオーバー")] public GameObject gameOverObj;
    [Header("ステージクリア")]public GameObject StageClearObj;
    [Header("ステージクリア判定")]public PlayerTriggerCheck stageClearTrigger;
     
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
