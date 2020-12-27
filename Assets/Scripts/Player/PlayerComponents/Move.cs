using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerComp;
/// <summary>
/// This script control player move 
/// </summary>
[RequireComponent(typeof(PlayerComponents))]
public class Move : MonoBehaviour, IUpdate, IStart, IFixedUpdate
{
    // Public data
    // **************************
    public int bugTimeCheck = 100;
    public float sideJumpMultipluyer;
    public float sideFallMultiplayer;
    public float Speed;
    public float runAnimOffset;
    // **************************
    Animator anim;
    PlayerComponents PC;
    PlayerDirection playerDirection;
    Jump jump;
    Raycasts raycasts;
    Rigidbody2D rigidBody;
    int runHash = Animator.StringToHash("MoveX");
    int boolRunHash = Animator.StringToHash("isRun");
    float _BugTimeCheck = 0;
    float lastMoveX;
    float jumpMoveDir;

    bool isMove;
    bool isRotate;
    
     
    float speed;
    bool isClean;
    private void FixedUpdate() {
        isClean = true;
    }
    public void ClearInput() {
        if (!isClean) return;
        speed = Speed;
        isClean = false;
    }
    public void DoStart() {
        PC = GetComponent<PlayerComponents>();
        anim = PC.animator;
        playerDirection = PC.playerDirection;
        rigidBody = PC.rigidbody2d;
        raycasts = PC.raycasts; 
        speed = Speed;
    }
    float moveX = 0;
    // Execute every frame. Check PlayerComponents.cs
    public void DoUpdate () {
        if (PC.dashScript.isDashing) return;
        ClearInput();
        // hehe... I don`t like it
        ref int jumpMoveDir = ref PC.jumpScript.jumpMoveDir;
        ref bool isCanMove = ref PC.jumpScript.isCanMove;
        ref bool isGrounded = ref raycasts.isGrounded;
        ref bool isSideHit = ref raycasts.isSideHit;

        moveX = PC.inputPlayerScript.MoveX;

        bool isKey = lastMoveX == moveX && (moveX != 1 && moveX != 0); // try resolve bug... It`s not work
        if (isKey && _BugTimeCheck > (bugTimeCheck / 1000)) {
            moveX = 0;
        } else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) {
            _BugTimeCheck += Time.deltaTime;
        } else {
            _BugTimeCheck = 0;
        }

        if (!isSideHit) {
            if (moveX > 0) 
                playerDirection.direction = -1;
            else if (moveX < 0) 
                playerDirection.direction = 1;
        }
        
        if (Mathf.Abs (moveX) >= runAnimOffset) {
            isMove = true;
        } else {
            isMove = false;
        }

        anim.SetFloat(runHash, moveX);
        if (moveX == 0 && lastMoveX == 0) {
            isRotate = false;
        } else if (Mathf.Abs(lastMoveX) - Mathf.Abs(moveX) >= runAnimOffset) {
            isRotate = true;
        } else if (Mathf.Abs(moveX)  >= runAnimOffset) {
            isRotate = false;
        }
        // if is moving, start run anim
        if (isMove || isRotate) {
            anim.SetFloat(runHash, Mathf.Abs(moveX));
            anim.SetBool(boolRunHash, true);
        } else {
            anim.SetBool(boolRunHash, false);
        }

        anim.SetBool("isSide", isSideHit);
        playerDirection.Rotate();
        if (moveX != 0) {
            isMove = true;
        }
        lastMoveX = moveX;
        anim.SetBool("isGrounded", isGrounded);
    }
    public void DoFixedUpdate() {
        if (PC.dashScript.isDashing) return;
        ref bool isGrounded = ref raycasts.isGrounded;
        ref bool isCanMove = ref PC.jumpScript.isCanMove;
        if (moveX != 0) {
            if (!isCanMove) {
                if (playerDirection.direction == -jumpMoveDir) {
                    // Jump in right direction
                    rigidBody.velocity = new Vector2(moveX * sideJumpMultipluyer * speed , rigidBody.velocity.y);
                } else {
                    // Jump in wrong direction
                    rigidBody.AddForce(new Vector2(moveX * speed, -sideFallMultiplayer));
                }
            } else {
                // Move player
                rigidBody.velocity = new Vector2(moveX * speed, rigidBody.velocity.y);
            }
        } else if (isGrounded) {
            // hehe... I don`t like this line, but it work
            rigidBody.velocity = new Vector2(0,rigidBody.velocity.y);
        }
    }
}
