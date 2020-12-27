using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCopy : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetData(Vector3 player_t, Quaternion rotation, Sprite player_s) {
        this.transform.position = player_t;
        this.transform.rotation = rotation;
        this.spriteRenderer.sprite = player_s;
    }
}
