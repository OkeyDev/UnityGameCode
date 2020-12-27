using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script controls money at the end of game
/// </summary>

public class MoneyScript : MonoBehaviour
{
    public string playerTag;
    public int addMoney = 0;
    public int addTime = 15;
    public float frequence;
    public float impulse;
    public GameObject particles;
    private LevelGenerator levelGenerator;
    private Vector3 startPos;
    private bool isLoad = false;
    private void Start() {
        startPos = transform.position;
    }
    private void Update() {
        transform.position = startPos + Vector3.up * Mathf.Sin(Time.time * frequence) * impulse;
    }
    
    public void SetLevelGenerator(LevelGenerator levelGenerator) {
        this.levelGenerator = levelGenerator;
    }
    /// <summary>
    /// If player touch it, it will destroy itself and go to generate level again
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (other.gameObject.tag == playerTag && !isLoad) {
            isLoad = true;
            Timer.instance.AddTime(addTime);
            UIController.AddScore(addMoney);
            levelGenerator.ContinueGenerate();
            Destroy(this.gameObject);
            GameObject obj = Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(obj, 1f);
        }
    }
}
