using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Setting : MonoBehaviour
{
    [SerializeField]
    public GameObject[] roomtype;
    List<int> roomList = new List<int>();

    void Awake()
    {
        for (int i = 0; i < 8;)
        {
            int currnum = Random.Range(0, 8);

            if (roomList.Contains(currnum))
            {
                currnum = Random.Range(0, 8);
            }
            else
            {
                roomList.Add(currnum);
                i++;
            }
        }
        for (int i = 0; i < 8; i++)
        {
            switch (roomList[i])
            {
                case 0:
                    Instantiate(roomtype[i], new Vector3(0f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f)).transform.parent = this.gameObject.transform;
                    break;
                case 1:
                    Instantiate(roomtype[i], new Vector3(0f, -30f, 0f), Quaternion.Euler(0f, 0f, 0f)).transform.parent = this.gameObject.transform;
                    break;
                case 2:
                    Instantiate(roomtype[i], new Vector3(0f, -60f, 0f), Quaternion.Euler(0f, 0f, 0f)).transform.parent = this.gameObject.transform;
                    break;
                case 3:
                    Instantiate(roomtype[i], new Vector3(22f, -94f, 0f), Quaternion.Euler(0f, 0f, -90f)).transform.parent = this.gameObject.transform;
                    break;
                case 4:
                    Instantiate(roomtype[i], new Vector3(-16f, -16f, 0f), Quaternion.Euler(0f, 0f, 90f)).transform.parent = this.gameObject.transform;
                    break;
                case 5:
                    Instantiate(roomtype[i], new Vector3(44f, -28f, 0f), Quaternion.Euler(0f, 0f, 180f)).transform.parent = this.gameObject.transform;
                    break;
                case 6:
                    Instantiate(roomtype[i], new Vector3(44f, -54f, 0f), Quaternion.Euler(0f, 0f, 180f)).transform.parent = this.gameObject.transform;
                    break;
                case 7:
                    Instantiate(roomtype[i], new Vector3(44f, -84f, 0f), Quaternion.Euler(0f, 0f, 180f)).transform.parent = this.gameObject.transform;
                    break;
                default:
                    break;
            }
        }
       }

    // Update is called once per frame
    void Update()
    {
    }
}
