using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cell : NinjaMonoBehaviour {
    public Grid Grid;
    public override string ToString() => "Column="+transform.position.x+" Row="+transform.position.y;
}
