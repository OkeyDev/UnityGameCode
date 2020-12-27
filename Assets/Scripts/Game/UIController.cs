using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// I hate this script... Why am I write it...
public class UIController : MonoBehaviour
{
    public static UIController Current;
    public Image dashReloadImage;
    public Image deatchPanel;
    public Canvas UICanvas;
    public Slider timeMovementReloadSlider;
    public Image[] images;
    public Text scoreText;
    int score = 0;
    // Start is called before the first frame update

    void Awake()
    {
        Current = this;
    }
    void Start () {
        Current = this;
    }
    public static bool SetHealth(float count)
    {
        if (Current == null) return false;
        //Debug.Log("count: " + count  + " Image length: " + Current.images.Length);
        if (count > Current.images.Length) {
            Debug.LogError("Health more than images");
            return false;
        }
        for (int i = 0;i < Current.images.Length;i++) {
            if (i < count) {
                Current.images[i].gameObject.SetActive(true);
            } else {
                Current.images[i].gameObject.SetActive(false);
            }
            
        }
        return true;
    }
    public static void ChangeDeathTextOpacity(float value) {
        
        //Current.deatchPanel.color = new Color(0, 0, 0, value);
    }
    public static void ChangeDeathPanelOpacity(float value) {
        if (Current.deatchPanel.gameObject.activeSelf == false) {
            Current.deatchPanel.gameObject.SetActive(true);
        }
        Current.deatchPanel.color = new Color(0, 0, 0, value);
    }
    public static void DisablePanel() {
        Current.deatchPanel.gameObject.SetActive(false);
    }
    public static void SetTimeMovementReloadSlider(float value) {
        Current.timeMovementReloadSlider.value = value;
    }
    public static void SetMaxTimeMovementReloadSlider(float value) {
        Current.timeMovementReloadSlider.maxValue = value;
        SetTimeMovementReloadSlider(value);
    }
    public static void DashReload (float value) {
        Current.dashReloadImage.fillAmount = value;
    }
    public static void AddScore(int value) {
        Current.score += value;
        Current.scoreText.text = Current.score.ToString();
    }
    public static int GetScore() {
        return Current.score;
    }
}
