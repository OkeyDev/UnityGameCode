using UnityEngine;
// struct for TimeMovement... Not using
public class PlayerData 
{
    public int player_health;
    public Vector3 player_position;
    public Quaternion player_rotation;
    public Sprite player_anim;

    public float rigidbody_X;

    public PlayerData(Transform transform, Sprite sprite, int health, float rigidbodyX) {
        player_position = transform.position;
        player_rotation = transform.rotation;
        player_anim = sprite;
        player_health = health;
        rigidbody_X = rigidbodyX;
    }
}
