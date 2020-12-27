using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerComp;
/// <summary>
/// Dash script
/// </summary>
[RequireComponent(typeof(PlayerComponents))]
public class Dash : MonoBehaviour, IUpdate, IFixedUpdate, IStart
{
	// *********
	public float DashSpeed;
    public float reloadDashTime;
    public float dashTime;
    public GameObject dashEffect;
    public ArrowDirection dashDirArrow;
	// *********
    PlayerComponents playerComponents;
    InputPlayer playerInput;
	PlayerDirection playerDirection;
    Animator anim;
    Rigidbody2D rigidBody;
    Vector2 _dashDirection;
	float dashReloadTime;
	float _dashTime;
    public bool isDashing;
    
    // Start is called before the first frame update
    public void DoStart()
    {
        // get all components
        playerComponents = GetComponent<PlayerComponents>();
		playerInput = playerComponents.inputPlayerScript;
		playerDirection = playerComponents.playerDirection;
        rigidBody = playerComponents.rigidbody2d;
        anim = playerComponents.animator;
    }
    public void DoFixedUpdate() {
        float DashDistance   = 10f;
        if (isDashing) {
                // if it is dashing we move player toward
                anim.SetBool("isDash", true);
                Vector2 dist = DashSpeed * _dashDirection * DashDistance;
                playerDirection.direction = -(int)Mathf.Sign(_dashDirection.x);
                float localDirection = playerDirection.direction == -1 ? 0 : 1;
                // and create effect
                GameObject _dashEffect =  Instantiate(dashEffect, transform.position, Quaternion.identity);
                _dashEffect.GetComponent<ParticleSystemRenderer>().flip = new Vector2(localDirection,0);
                Destroy(_dashEffect, 0.4f);
                rigidBody.velocity = dist;
                _dashTime -= Time.fixedDeltaTime;
                //rigidBody.AddForce(new Vector2(dist, 0), ForceMode2D.Impulse);
            }
        if (_dashTime <= 0 && isDashing) {
                // stop dash and set values to default
                anim.SetBool("isDash", false);
                isDashing = false;
                dashReloadTime = 0;
                _dashDirection = Vector2.zero;
                rigidBody.velocity = Vector2.zero;
            }
    }
    // Update is called once per frame
    public void DoUpdate()
    {
        int isDash         = playerInput.isDash;
        ref bool isSlowMotionStarted = ref playerComponents.slowMotion.isSlowMotionStarted; // if it`s a direction dash
        if (dashReloadTime >= reloadDashTime) {
            if (isDash != 0 && !isDashing && !isSlowMotionStarted) 
            {
                // start usual dash
                isDashing = true;
                _dashTime = dashTime;
                _dashDirection = new Vector2(isDash, 0);
                playerDirection.direction = -isDash;
                playerDirection.Rotate();
                
            } 
            if (isSlowMotionStarted && Input.GetMouseButton(0) && !isDashing) {
                // start direction dash
                isDashing = true;
                _dashTime = dashTime;
                _dashDirection = dashDirArrow.GetNormalizedRotation();
                
                playerComponents.slowMotion.StopSlowMotion();
                playerDirection.Rotate();
            }
        }
        else {
            // or update reload time 
            dashReloadTime += Time.deltaTime; 
        }
        UIController.DashReload(dashReloadTime / reloadDashTime);
    }
}
