using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// i don`t use this script
public class SwordForward : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float rotateSpeed;
    public float startPosRotateSpeed;

    public float forwardDistance;
    public Transform target;
    float this_speed;
    public SpriteRenderer swordSprite;
    void Update()
    {
        
        float dist = Vector2.Distance(transform.position,target.position);
        this_speed = Mathf.Min(dist * speed, maxSpeed);
        if (dist > forwardDistance){
            Vector3 relativePos = target.position - transform.position;
            float angel = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angel, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
            
            //transform.up = relativePos;
        } else {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0,0,-90), startPosRotateSpeed * Time.deltaTime);
        }

        
        //Quaternion rotation1 = Quaternion.LookRotation(transform.position - target.position, Vector3.left );
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation1, Rspeed);
		//transform.rotation = Quaternion.LookRotation(relativePos, Vector3.zero);
        transform.position = Vector2.MoveTowards(transform.position, target.position, this_speed * Time.deltaTime);
   
    }
    public void DisableSword(){
        swordSprite.enabled = false;
    }
    public void EnableSword(){
        if (!swordSprite.enabled) this.transform.position = target.position;
        swordSprite.enabled = true;
    }

}
