using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EventManager : MonoBehaviour
{
    GameObject button;
    public GameObject Lhand, Rhand;

    void Update()
    {
        Lhand.GetComponent<Image>().sprite = GameObject.FindWithTag("LBt").transform.GetChild(0).GetComponent<Image>().sprite;
        Rhand.GetComponent<Image>().sprite = GameObject.FindWithTag("RBt").transform.GetChild(0).GetComponent<Image>().sprite;
    }

    // 버튼 일반 클릭
    public void ButtonClick()
    {
        button = EventSystem.current.currentSelectedGameObject;
        print(button.name);
    }

    private bool isLButtonPressed; // 클릭 중인지 판단 

    // 버튼 클릭이 시작했을 때
    public void LButtonDown()
    {
        isLButtonPressed = true;
    }

    // 버튼 클릭이 끝났을 때
    public void LButtonUp()
    {
        isLButtonPressed = false;
    }

    private bool isRButtonPressed; // 클릭 중인지 판단 

    // 버튼 클릭이 시작했을 때
    public void RButtonDown()
    {
        isRButtonPressed = true;
    }

    // 버튼 클릭이 끝났을 때
    public void RButtonUp()
    {
        isRButtonPressed = false;
    }

    private bool isInterPressed; // 클릭 중인지 판단 

    // 버튼 클릭이 시작했을 때
    public void InterDown()
    {
        isInterPressed = true;
    }

    // 버튼 클릭이 끝났을 때
    public void InterUp()
    {
        isInterPressed = false;
    }
    public bool GetLpressed()
    {
        return isInterPressed;
    }
    public bool GetRpressed()
    {
        return isInterPressed;
    }

    public bool GetpressedInter()
    {
        return isInterPressed;
    }
}
