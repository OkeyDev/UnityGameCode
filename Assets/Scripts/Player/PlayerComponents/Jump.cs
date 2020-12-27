using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerComp;
/// <summary>
/// This script control player jump
/// </summary>
public class Jump : MonoBehaviour, IUpdate, IStart
{
    // **********
    public float JumpLength;
    public float sideJumpX = 1f;
    public float sideJumpY = 1f;
    public float waitAfterJump = 0.1f;
    public float fallMultiplier = 0.6f;
    // Side jump drag. When that collision with walls, which uses for side jump
    public float StartDragWhenSide = 10f;
    public float EndDragWhenSide = 0.2f;
    public float DragMultiplayer;
    public ParticleSystem dust;
    int jumpHash = Animator.StringToHash("velocitY");
	
    public LayerMask groundLayer;
    public LayerMask sideJumpGridLayer;

    // **********
    PlayerComponents playerComponents;
    InputPlayer playerInput;
    PlayerDirection  playerDirection;
    Raycasts raycasts;
    Animator anim;
    Rigidbody2D rigidBody;
    bool isDrag;
    bool isDoubleJump;
    [HideInInspector]
    public bool isCanMove = true;
    float dragValue;
    [HideInInspector]
    public int jumpMoveDir;
    // Start is called before the first frame update
    public void DoStart()
    {
        playerComponents = GetComponent<PlayerComponents>();
        playerInput = playerComponents.inputPlayerScript;
        rigidBody = playerComponents.rigidbody2d;
        playerDirection = playerComponents.playerDirection;
        raycasts = playerComponents.raycasts;
        anim = playerComponents.animator;

    }

    // Update is called once per frame
    public void DoUpdate()
    {
        // That was not good idea...
        ref bool isGrounded = ref raycasts.isGrounded;
        ref bool isSideHit = ref raycasts.isSideHit; // Первый вариант - ссылка  
        ref bool isLSideHit = ref raycasts.isLSideHit; 

        KeyCode JumpButon = playerInput.GetJumpKeycode();
        // try jump
        if (isGrounded && Input.GetKeyDown(JumpButon) )
        {
            // just jump
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, JumpLength);
            dust.Play();
        }
        else if (isSideHit && !isGrounded && Input.GetKeyDown(JumpButon)) {
            // wall jump
            int localDirection = isLSideHit ? 1 : -1;
            isDrag = false;
            rigidBody.drag = EndDragWhenSide;
            rigidBody.velocity = Vector2.zero;
            rigidBody.AddForce(new Vector2(sideJumpX * localDirection, sideJumpY ), ForceMode2D.Impulse);
            jumpMoveDir = localDirection;
            StartCoroutine(WaitAfterJumpFunc());
        }
        /*if (isJumping  && Input.GetKey(JumpButon)) {
            if (JumpTimeCounting > 0) {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, holdJumpLength);
                JumpTimeCounting -= Time.deltaTime;
            } else {
                isJumping = false;
            }
        } //Jump higher by holding*/ 
        
        if (!isGrounded) { // if not on ground
            if (isSideHit && !isDrag) { // start drag on side hit
                isDrag = true;
                rigidBody.gravityScale = 1f;
                dragValue = 2;
            } else if (!isSideHit) { // if not side hit
                isDrag = false;
                dragValue = 0;
                rigidBody.gravityScale = 1;
                rigidBody.drag = 4f * 0.15f;
                if(rigidBody.velocity.y < 0){
                    rigidBody.gravityScale = 1 * fallMultiplier;
                }else if(rigidBody.velocity.y > 0 && !Input.GetButton("Jump")){
                    rigidBody.gravityScale = 1 * (fallMultiplier / 2);
                }
            }
        } else {
            // if on ground reload double jump
            isDoubleJump = false;
        }
        if (isDrag && (rigidBody.velocity.y < 0)) { // Side drag
            dragValue -= Time.deltaTime * DragMultiplayer;
            playerDirection.direction = isLSideHit ? 1 : -1;
            float localDrag = Mathf.Clamp01( dragValue);
            rigidBody.drag = Mathf.Lerp(StartDragWhenSide, EndDragWhenSide, 1 - localDrag);
        } 
        if (!isGrounded && Input.GetKeyDown(JumpButon) && !isDoubleJump) { // Double jump
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, JumpLength);
            isDoubleJump = true;
        }
        anim.SetFloat(jumpHash, rigidBody.velocity.y);
    }
    IEnumerator WaitAfterJumpFunc () {
        isCanMove = false;
        yield return new WaitForSeconds(waitAfterJump);
        isCanMove = true;
    }
}
