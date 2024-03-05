using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    float p_status;
    public bool isRun;
    public bool isSlow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRun)
        {
            if (isSlow)
                p_status = 1.15f;
            else p_status = 1.5f;
        }
        else
        {
            if (isSlow)
                p_status = 0.75f;
            else p_status = 1f;
        }
        StateCheck();
        PlayerHMove();
        PlayerVMove();
    }

    void StateCheck()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRun = true;
        }
        else
        {
            isRun = false;
        }
    }

    void PlayerHMove()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
        }
        transform.position += moveVelocity * speed * p_status * Time.deltaTime;
    }
    void PlayerVMove()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            moveVelocity = Vector3.down;
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            moveVelocity = Vector3.up;
        }
        transform.position += moveVelocity * speed * p_status * Time.deltaTime;
    }
}
