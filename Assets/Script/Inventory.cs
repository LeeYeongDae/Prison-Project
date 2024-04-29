using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items;

    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private Slot[] slots;
    public bool invenFull;

#if UNITY_EDITOR
    private void OnValidate()       //Bag 넣으면 slot 자동 등록
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }
#endif

    void Awake()
    {
        FreshSlot();                //시작 시 items의 아이템 인벤토리 넣기
    }

    void Update()
    {
        if (items.Count >= slots.Length)
            invenFull = true;
        else
            invenFull &= !invenFull;
    }

    public void FreshSlot()
    {
        int i = 0;
        for (; i < items.Count && i < slots.Length; i++)
            slots[i].item = items[i];
        for (; i < slots.Length; i++)
            slots[i].item = null;
    }

    public void AddItem(Item _item)
    {
        if (items.Count < slots.Length)
        {
            items.Add(_item);
            FreshSlot();
        }
        else
        {
            print("슬롯이 가득 찼습니다.");
        } 
    }
}
