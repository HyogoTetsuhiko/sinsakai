using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    [Header("これを踏んだ時の高さ")] public float boundHeight;

    //プレイヤーと踏んだ物の橋渡しの役割
    //HideInInspectorとはインスペクターにシリアライズ化されたものが表示されなくなる
    [HideInInspector]public bool playerStepOn;
}
 