using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemy : EnemyClassWait
{
    public CircleCollider2D circleCollider;
    public LineRenderer lineRender;
    public float thetaScale = 0.01f;
    public float radius = 0.8f;
    public Gradient attackGradient;
    bool CheckSwordAttack() {
        Vector2 origin = circleCollider.transform.position;
        return Physics2D.CircleCast(origin, circleCollider.radius, Vector2.right * _direction, 0 ,playerLayer);
    }
    protected override void AfterCheckAttack() {
        if (CheckSwordAttack()) {
            StartWait();
        }
    }
    protected override void Attack(PlayerHealth playerHealth) {
        if (CheckSwordAttack()) {
            playerHealth.GetDamage();
        }
    }
    protected override void AfterUpdate() {
        float theta = 0f;
        int size = (int)((1f / thetaScale) + 1f);
        lineRender.positionCount = size; 
        Color color = attackGradient.Evaluate(_waitBeforeAttack * 1/waitBeforeAttack);
        lineRender.startColor = color;
        lineRender.endColor = color;
        for(int i = 0; i < size; i++){          
            theta += (2.0f * Mathf.PI * thetaScale);         
            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta);          
            lineRender.SetPosition(i, circleCollider.gameObject.transform.position + new Vector3(x, y, 0));
        }
    }
}
