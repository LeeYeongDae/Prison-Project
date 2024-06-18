using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IObjectItem
{
    Item Obitem();
}

public class Interaction : MonoBehaviour, IObjectItem
{
    Inventory inven;
    Player player;
    private bool interacting;
    public Slider workGage;
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            if (this.gameObject.tag == "Item")
                workGage.gameObject.SetActive(true);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (this.gameObject.tag == "Item" && player.isInteracting)
            {
                workGage.value += Time.deltaTime;
                if (!inven.invenFull && workGage.value == workGage.maxValue)
                    this.ObtainItem(obtem);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (this.gameObject.tag == "Item")
            {
                workGage.gameObject.SetActive(false);
                workGage.value = 0;
            }
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
