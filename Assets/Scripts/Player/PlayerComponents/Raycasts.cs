using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerComp;
/// <summary>
/// This script controls all player raycasts.
/// </summary>
// Like ground check, side check, etc
[RequireComponent(typeof(PlayerComponents))]
public class Raycasts : MonoBehaviour, IUpdate, IStart
{
    public Vector2 groundOffset;
    public Vector2 underGroudOffset;
    public Vector2 sideCheckOfsset;
    public Vector2 sideCheckSize;
    public float groundOffsetPlus;
    public bool isGrounded, isDoubleJump, isGroundUnderMe, isRSideHit, isLSideHit;
    public bool isSideHit;
    public float underGroudLength;
    public float length;
    public LayerMask groundLayer;
    public LayerMask sideJumpGridLayer;
    PlayerComponents playerComponents;
    PlayerDirection playerDirection;

    bool isClean = false;
    private void FixedUpdate() {
        isClean = true;
    }
    public void ClearInput() {
        if (!isClean) return;
        isGrounded = false;
        isRSideHit = isLSideHit = isSideHit = false;

        isClean = false;
    }
    // Start is called before the first frame update
    public void DoStart()
    {
        playerComponents = GetComponent<PlayerComponents>();
        playerDirection = playerComponents.playerDirection;
    }

    // Update is called once per frame
    public void DoUpdate()
    {
        ClearInput();
        RaycastHit2D leftHit  = MyPhysics.Raycast(transform.position,new Vector2((groundOffset.x + groundOffsetPlus) * playerDirection.direction, groundOffset.y), Vector2.down, length, groundLayer);
        RaycastHit2D rightHit = MyPhysics.Raycast(transform.position,new Vector2((-groundOffset.x + groundOffsetPlus) * playerDirection.direction, groundOffset.y), Vector2.down, length, groundLayer);
        if (leftHit || rightHit) {isGrounded = true;  isDoubleJump = false; }
        isGroundUnderMe = MyPhysics.Raycast(transform.position,underGroudOffset, Vector2.down, underGroudLength, groundLayer);
		// Check side collision
        RaycastHit2D sideRHit = MyPhysics.BoxCast((Vector2)transform.position, new Vector2(-sideCheckOfsset.x, sideCheckOfsset.y), sideCheckSize, Vector2.right, sideJumpGridLayer );
		RaycastHit2D sideLHit = MyPhysics.BoxCast((Vector2)transform.position, new Vector2( sideCheckOfsset.x, sideCheckOfsset.y), sideCheckSize, Vector2.left, sideJumpGridLayer );
        // save to global vars
        isRSideHit = sideRHit;
        isLSideHit = sideLHit;
        isSideHit = isRSideHit || isLSideHit;
    }
}
