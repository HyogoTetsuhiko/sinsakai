using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private string groundTag = "Ground";
    private bool isGround = false;
    private bool isGroundEnter,isGroundStay,isGroundExit;
    // Start is called before the first frame update

    public bool IsGround()
    {
        if (isGroundEnter || isGroundStay)
        {
            isGround = true;
        }
        else if(isGroundExit)
        {
            isGround = false;
        }
        isGroundEnter = false;
        isGroundStay = false;
        isGroundExit = false;
        return isGround;
     }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            isGroundEnter = true;
            Debug.Log("地面が判定にはいりました。");
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            isGroundStay = true;
            Debug.Log("地面が判定に入り続けています。");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            isGroundExit = true;
            Debug.Log("地面が判定をでました。");
        }
    }
}
