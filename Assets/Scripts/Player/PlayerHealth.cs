using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int godModeTime;
    int health;
    bool godMode;
    Rigidbody2D rigidBody;
    Death deathScript;
    //Move playerMove;
    void Start()
    {
        //playerMove = GetComponent<Move>();
        rigidBody = GetComponent<Rigidbody2D>();
        health = maxHealth;
        UIController.SetHealth(health);
        deathScript = GetComponent<Death>();
    }
    public void SetHealth(int numb) {
        health = numb;
        UIController.SetHealth(health);
    }
    public void GetDamage()
    {
        if (godMode) return;
        health -= 1;
        UIController.SetHealth(health);
        if (health <= 0) {
            deathScript.Respawn();
            ResetHeath();
            return;
        }
        godMode = true;
        //playerMove.MoveBack();
        StartCoroutine(GodModeTime());
    }
    public int GetHealth() {
        return health;
    }
    IEnumerator GodModeTime() {
        float count = 0;
        while (count < godModeTime) {
            yield return new WaitForSeconds(1);
            count += 1;
        }
        godMode = false;
    }
    public void ResetHeath() {
        health = maxHealth;
        UIController.SetHealth(health);
    }
}
