using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    float p_status;
    public bool isRun;
    public bool isSlow;
    public bool isInteracting;
    public bool PressIBt;
    float PressTime = 0f;
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
        Interact_StateCheck();
        Run_StateCheck();
        PlayerHMove();
        PlayerVMove();
    }

    void Run_StateCheck()
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

    void Interact_StateCheck()
    {
        if(Input.GetKey(KeyCode.E) || PressIBt)
        {
            isInteracting = true;
            PressTime += Time.deltaTime;
        }
        else
        {
            isInteracting = false;
        }

        if (PressTime > 0.1f)
            PressIBt = false;
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
    public void DoInteraction()
    {
        PressIBt = true;
    }
}
