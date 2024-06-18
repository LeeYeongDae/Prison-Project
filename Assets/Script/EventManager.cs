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

    // ��ư �Ϲ� Ŭ��
    public void ButtonClick()
    {
        button = EventSystem.current.currentSelectedGameObject;
        print(button.name);
    }

    private bool isLButtonPressed; // Ŭ�� ������ �Ǵ� 

    // ��ư Ŭ���� �������� ��
    public void LButtonDown()
    {
        isLButtonPressed = true;
    }

    // ��ư Ŭ���� ������ ��
    public void LButtonUp()
    {
        isLButtonPressed = false;
    }

    private bool isRButtonPressed; // Ŭ�� ������ �Ǵ� 

    // ��ư Ŭ���� �������� ��
    public void RButtonDown()
    {
        isRButtonPressed = true;
    }

    // ��ư Ŭ���� ������ ��
    public void RButtonUp()
    {
        isRButtonPressed = false;
    }

    private bool isInterPressed; // Ŭ�� ������ �Ǵ� 

    // ��ư Ŭ���� �������� ��
    public void InterDown()
    {
        isInterPressed = true;
    }

    // ��ư Ŭ���� ������ ��
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
