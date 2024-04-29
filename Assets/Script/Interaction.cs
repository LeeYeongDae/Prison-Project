using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectItem
{
    Item Obitem();
}

public class Interaction : MonoBehaviour, IObjectItem
{
    Inventory inven;
    Player player;
    [SerializeField]
    public Item obtem;

    void Awake()
    {
        inven = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    private void Update()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && player.isInteracting)
        {
            if (!inven.invenFull)
                this.ObtainItem(obtem);
        }
    }

    public void ObtainItem(Item item)
    {
        inven.AddItem(item);
        this.gameObject.SetActive(false);   //Arrest 되기 전까지 재획득 불가능
    }

    public Item Obitem()
    {
        return this.obtem;
    }
}
