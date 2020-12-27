using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPhysics : MonoBehaviour
{
    
    public static RaycastHit2D Raycast (Vector2 pos,Vector2 offset, Vector2 rayDirection, float length, LayerMask mask, bool drawDebugRaycasts=true) {

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
    public static RaycastHit2D BoxCast (Vector2 pos, Vector2 offset, Vector2 size, Vector2 direction, LayerMask layer) {
        
        RaycastHit2D data = Physics2D.BoxCast(pos + offset, size, 0, direction, 0, layer );
        Color red = new Color(255,0,0);

        Vector2 topLeft = new Vector2(pos.x + offset.x , pos.y + offset.y + size.y/2);
        Vector2 topRight = new Vector2(pos.x + offset.x + size.x * direction.x, pos.y + offset.y + size.y/2);
        Vector2 bottomLeft = new Vector2(pos.x + offset.x, pos.y - offset.y - size.y/2);
        Vector2 bottomRight = new Vector2(pos.x + offset.x + size.x * direction.x, pos.y - offset.y - size.y/2);
        
        Debug.DrawLine(topLeft,topRight, red, 0.1f);
        Debug.DrawLine(bottomLeft, bottomRight, red, 0.1f);
        Debug.DrawLine(topLeft, bottomLeft, red, 0.1f);
        Debug.DrawLine(topRight, bottomRight, red, 0.1f);
        Debug.DrawLine(topLeft, bottomRight, new Color(0,0,255));
        return data;
    } 
}
