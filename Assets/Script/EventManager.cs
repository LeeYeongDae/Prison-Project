using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    Player player;
    void Update()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void Interaction()
    {
        player.DoInteraction();
    }
}
