using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndSceneController : MonoBehaviour
{
    public Text scoreText;
    public string restartScene;
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) {
            Restart();
        }
    }
    /// <summary>
    /// Load score from DataHolder
    /// </summary>
    /// <returns>score</returns>
    int LoadData () {
        return DataHolder.instance.score;
    }
    private void Start() {
        scoreText.text = "Your score: " + LoadData ().ToString();
    }
    /// <summary>
    /// Load game scene
    /// </summary>
    public void Restart () {
        DataHolder.instance.score = 0;
        SceneManager.LoadScene(restartScene);
    }
    /// <summary>
    /// Exit from game
    /// </summary>
    public void Exit () {
        Application.Quit();
    }
}
