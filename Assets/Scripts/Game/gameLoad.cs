using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// Delete it
public class gameLoad : MonoBehaviour
{
    public string playerTag = "Player";
    public string loadScene;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == playerTag) {
            SceneManager.LoadScene(loadScene);
        }
    }
}
