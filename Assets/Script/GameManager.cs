using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Text DutyTxT;
    public Text TimeTxT;

    public int GuardOnDuty;

    public bool isClear;
    public bool isOver;

    public int LifeCount;



    float inTime = 0, min = 0;
    int hour;
    bool pm;

    public int dutyTime;    // 0 = Free, 1 = Work, 2 = Eat, 3 = Sleep, 4 = Moving[업무간 이동]
    public int guardOnJob;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameObject("GameManager").AddComponent<GameManager>();
            return instance;
        }
    }

    private void Awake()
    {
        GameObject go = GameObject.Find("GameManager");
        if (go == null)
        {
            go = new GameObject { name = "GameManager" };
            go.AddComponent<GameManager>();
        }
        DontDestroyOnLoad(go);
        instance = go.GetComponent<GameManager>();

        Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        hour = 7;
        pm = false;
    }

    // Update is called once per frame
    void Update()
    {
        inTime += 1.5f * Time.deltaTime;
        if (inTime >= 900f) dutyTime = 3;
        else if (inTime >= 720f) dutyTime = 0;
        else if (inTime >= 660f) dutyTime = 2;
        else if (inTime >= 480f) dutyTime = 1;
        else if (inTime >= 360f) dutyTime = 0;
        else if (inTime >= 300f) dutyTime = 2;
        else if (inTime >= 120f) dutyTime = 1;
        else if (inTime >= 60f) dutyTime = 2;
        else dutyTime = 0;

        if (inTime >= 1440f) inTime -= 1440f;

        min += 1.5f * Time.deltaTime;
        if (min >= 60f)
        {
            hour += 1;
            min -= 60;
        }
        if (hour >= 12)
        {
            pm = !pm;
            hour -= 12;
        }
        GetDuty();
        GetTime();
    }

    public void Init()
    {
        
    }
    public void GetDuty()
    {
        switch(dutyTime)
        {
            case 0:
                DutyTxT.text = string.Format("자유시간");
                break;
            case 1:
                DutyTxT.text = string.Format("업무시간");
                break;
            case 2:
                DutyTxT.text = string.Format("식사시간");
                break;
            case 3:
                DutyTxT.text = string.Format("수면시간");
                break;
            default:
                break;
        }
    }
    public void GetTime()
    {
        if(pm)
        {
            if (hour == 0)
                TimeTxT.text = string.Format("PM {0:D2}:{1:D2}", hour+12, (int)min);
            else
                TimeTxT.text = string.Format("PM {0:D2}:{1:D2}", hour, (int)min);
        }
        else
        {
            TimeTxT.text = string.Format("AM {0:D2}:{1:D2}", hour, (int)min);
        }
    }
}
