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
    private void OnValidate()       //Bag ������ slot �ڵ� ���
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }
#endif

    void Awake()
    {
        FreshSlot();                //���� �� items�� ������ �κ��丮 �ֱ�
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
            print("������ ���� á���ϴ�.");
        } 
    }
}
