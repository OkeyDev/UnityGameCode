using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    public float die = -15f;
    public string sceneName;
    public Transform respawnPos;
    private void Update() {
        if (sceneName == "") return;
        if (transform.position.y < die) {
            // TODO: redo this on normal end
            Timer.instance.EndGame();
        }
    }
    public void Respawn () {
        transform.position = respawnPos.position;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (respawnPos == null) return;
        if (other.tag == "Respawn") {
            Respawn ();
        }
    }
}
