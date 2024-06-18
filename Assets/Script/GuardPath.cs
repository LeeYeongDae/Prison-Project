using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GuardPath : MonoBehaviour
{
    public GameObject CurPin;

    public Vector2 startPos, targetPos;

    private Vector2 currPosition, destiPos;
    public float speed = 10f;
    public bool isFixd = false; //목적지 도착 시 이동 X
    GameObject Destination;

    GameObject sight;

    int dutytime;


    void Start()
    {
        targetPos = this.gameObject.transform.position;
        sight = transform.GetChild(1).gameObject;
    }

    void Update()
    {
        startPos = this.gameObject.transform.position;

        //if (startPos == targetPos)
        //{
        //    if(dutytime ==0)
        //    {
                
        //    }
        //    else if (dutytime == 1)
        //    {
        //        destinum = 22 | 24; //종료시간까지 홀딩, 업무 미완료 시 검문모드
        //    }
        //    else if (dutytime == 2)
        //    {
        //        destinum = 25 | 26; //종료시간까지 홀딩, 순찰모드
        //    }
        if ((Vector2)CurPin.transform.position == startPos)
        {
            Destination = CurPin.gameObject.GetComponent<PathPin>().GetNextPin();
            destiPos = Destination.transform.position;
        }

        Vector3 dir = destiPos - currPosition;
        Vector3 qut = Quaternion.Euler(0, 0, -90) * dir;
        Quaternion rot = Quaternion.LookRotation(forward: Vector3.forward, upwards: qut);
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, rot, Time.deltaTime * 300f);

        GuardMove();
    }

    void FixedUpdate()
    {
        dutytime = GameObject.Find("GameManager").GetComponent<GameManager>().dutyTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Path")
            CurPin = collision.gameObject;
    }

    void GuardMove()
    {
        currPosition = startPos;
        float walk = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(currPosition, destiPos, walk);
        if (currPosition == destiPos)
        {
            CurPin = Destination;
        }
    }
}