using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerComp;
/// <summary>
/// This script control player direction
/// </summary>
// Yeah, it`s script...
[RequireComponent(typeof(PlayerComponents))]
public class PlayerDirection : MonoBehaviour
{
    public int direction = 0;
    public void Rotate() {
        int localDirection = direction == -1 ? 0 : 1;
        transform.rotation = Quaternion.Euler(0, 180 * localDirection, 0);
    }
    public void Rotate(int dir) {
        int localDirection = dir == -1 ? 0 : 1;
        transform.rotation = Quaternion.Euler(0, 180 * localDirection, 0);
    }
}
