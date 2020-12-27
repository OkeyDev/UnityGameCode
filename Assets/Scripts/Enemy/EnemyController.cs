using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool drawDebugRaycasts;

    public float Speed;
    public float raycastLength;
    public float raycastBoxLength;
    public Vector2 offsetGroundTouch;
    public LayerMask groundLayer;
    public LayerMask playerLayer;

    float speed;
    int Direction = 1;
    Rigidbody2D rigidBody;
    Collider2D playerCollider;
    // Start is called before the first frame update
    void Start()
    {
        speed = Speed; // Set user speed
        rigidBody = GetComponent<Rigidbody2D>(); // Get rigidbody component
        playerCollider = GetComponent<Collider2D>();
    }

    void Translate() {
        if (rigidBody.velocity.y < 0) {
            speed = 1; //  move, if player is falling down
        }
        transform.Translate(Vector2.right * Direction * speed * Time.deltaTime); //No Physics
    }
    void Update()
    {
        speed = Speed; // Start Value
        Translate(); // Move enemy
        
    }
    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length)
    {
        //Call the overloaded Raycast() method using the ground layermask and return 
        //the results
        return Raycast(offset, rayDirection, length, groundLayer);
    }

    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask mask)
    {
        //Record the player's position
        Vector2 pos = transform.position;

        //Send out the desired raycasr and record the result
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, mask);

        //If we want to show debug raycasts in the scene...
        if (drawDebugRaycasts)
        {
            //...determine the color based on if the raycast hit...
            Color color = hit ? Color.red : Color.green;
            //...and draw the ray in the scene view
            Debug.DrawRay(pos + offset, rayDirection * length, color);
        }

        //Return the results of the raycast
        return hit;
    }
}
