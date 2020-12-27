using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
// Delete it
public class HideLayerScript : MonoBehaviour
{
    Animator animator;
    int hash;
    void Start()
    {
        animator = GetComponent<Animator>();
        hash = Animator.StringToHash("IsSee");   
        // 1. Я добавил двойной прыжок и усиление, усли задерживаешь
        // 2. А также сделал спрятанные зоны
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Enter");
        animator.SetBool(hash,true);
    }
    private void OnTriggerExit2D(Collider2D other) {
        Debug.Log("Exit");
        animator.SetBool(hash,false);
    }
}
