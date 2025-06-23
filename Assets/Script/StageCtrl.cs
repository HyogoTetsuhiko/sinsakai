using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCtrl : MonoBehaviour
{
    [Header("�v���C���[�Q�[���I�u�W�F�N�g")] public GameObject playerObj;
    [Header("�R���e�B�j���[�ʒu")] public GameObject[] countinuePoint;

    private Player p;
    void Start()
    {
        if(playerObj != null && countinuePoint != null && countinuePoint.Length > 0)
        {
            playerObj.transform.position = countinuePoint[0].transform.position;

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

    }
}
