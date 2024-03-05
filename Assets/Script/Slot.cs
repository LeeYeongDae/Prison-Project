using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] Image image;

    private Item _item;
    public Item item
    {
        get { return _item; }   //슬롯의 아이템 정보 리턴
        set
        {
            _item = value;      //item 정보 저장
            if (_item != null )
            {
                image.sprite = item.itemImage;
                image.color = new Color( 1, 1, 1, 1);
            }
            else
                image.color = new Color(1, 1, 1, 0);
        }
    }
}
