using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Delete it
public class GameController : MonoBehaviour
{
    static PlayerHealth playerHealth;
    public PlayerHealth _playerHealth;

    void OnEnable()
    {
        if (_playerHealth != null) playerHealth = _playerHealth;
    }
    public static void EndGame() {
        Debug.Log("END"); 
    }
    public static PlayerHealth GetPlayerHealth() {
        return playerHealth;
    }
}
