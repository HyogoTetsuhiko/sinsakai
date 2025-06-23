using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCtrl : MonoBehaviour
{
    [Header("プレイヤーゲームオブジェクト")] public GameObject playerObj;
    [Header("コンティニュー位置")] public GameObject[] countinuePoint;

    private Player p;
    void Start()
    {
        if(playerObj != null && countinuePoint != null && countinuePoint.Length > 0)
        {
            playerObj.transform.position = countinuePoint[0].transform.position;

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

    }
}
