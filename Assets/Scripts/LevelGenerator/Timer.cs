using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public static Timer instance {get {
        return _instance;
    }}
    private static Timer _instance;
    public int defaultTime;
    public string endSceneName = "EndScene";
    private int time;
    public Text timerText;
    // public
    public void AddTime (int addTime) {
        time += addTime;
        UpdateScore();
    }
    // private
    void Awake() {
        _instance = this;
    }
    void UpdateScore() {
        timerText.text = time.ToString();
    }
    private void Start() {
        time = defaultTime;
        StartCoroutine("updateTime");
    }
    IEnumerator updateTime () {
        while (time >= 0) {
            UpdateScore();
            time -= 1;
            yield return new WaitForSeconds(1);
        }
        EndGame();
    }
    public void EndGame() {
        Save();
        SceneManager.LoadScene(endSceneName);
    }
    void Save () {
        DataHolder.instance.score = UIController.GetScore();
    }
}
