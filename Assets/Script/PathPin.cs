using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pin
{
    public Pin(int _x, int _y) {x = _x; y = _y; }

    public Pin Neighbor;

    public int x, y;
}
