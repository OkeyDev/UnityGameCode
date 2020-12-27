using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// OH YEAH!! THIS 500 lines of code place in PlayerComponents folder
public class Controller : MonoBehaviour
{
    public bool drawDebugRaycasts;
    [Header("Player")]
    InputPlayer PlayerInput;
    public Rigidbody2D rigidBody;
    Animator anim;

    [Header("Movement")]
    public int bugTimeCheck = 100;
    public float bugSensitive = 3;
    public float Speed;
    public float length;
    public float GroundOffsetPlus;
    public float runOffset;
    public Vector2 GroundOffset;
    public float fallMultiplier;
    
    
    int runHash = Animator.StringToHash("MoveX");
    int boolRunHash = Animator.StringToHash("isRun");

    [Header("Jump")]
    public float sideFallMultiplayer;
    public float sideJumpMultipluyer;
    public float JumpLength;
    public float sideJumpX = 1f;
    public float sideJumpY = 1f;
    public float waitAfterJump = 0.1f;
    // Side jump drag. When that collision with walls, which uses for side jump
    public float StartDragWhenSide = 10f;
    public float EndDragWhenSide = 0.2f;
    public float DragMultiplayer;
    // *
    float dragValue;
    bool isDrag;
	public Vector2 sideCheckOfsset;
    public Vector2 sideCheckSize;
    int jumpHash = Animator.StringToHash("velocitY");
	
    public LayerMask groundLayer;
    public LayerMask sideJumpGridLayer;

    [Header("Attack")]
    public Transform Sword;
    public float SwordRadius;
    public float AttackDelay;
    public LayerMask enemyLayer;
    //public SwordForward///swordScript;

    [Header("Dash")]
    public float DashSpeed;
    public float ReloadDashTime;
    public float dashTime;
    public GameObject dashEffect;
    public ArrowDirection dashDirArrow;

    [Header("Time Movement")]
    public float timeCatch;
    public float underGroudLength;
    public Vector2 underGroudOffset;
    public float reloadAbility;
    public float addAbilityOnAttack;
    public bool reloadAbilityEnable;
    [Header("SlowMotion")]
    public float changeToTimeScale; 
    public float slowMotionMultiplayer = 2f;
    public float slowMotionReloadTime;
    public float slowMotionEnd;
    private float _slowMotionEnd;
    private float _slowMotionReloadTime;
    bool isSlowMotionStarted;
    
    
    
    



    bool isClean;
    bool isCanMove = true;
    int jumpMoveDir;
	bool isRSideHit;
    bool isLSideHit;
    bool isGrounded;
    bool isGroundUnderMe;
    //bool isRunning;
    bool isMove;
    bool isRotate; // Variable for MovePlayer func. 
    bool isAlreadyJump;
    bool isDoubleJump;
    bool isDashing;
    bool isAttacking;
    float lastMoveX; // Variable for MovePlayer func. 
    float JumpTimeCounting;
    float speed;
    float TimingAttack;
    float _dashTime;
    Vector2 _dashDirection;
    float DashReloadTime;
    float reloadAbilityTime;
    int direction;
    
    SpriteRenderer PlayerSprite;
    PlayerHealth healthPlayer;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        PlayerInput  = GetComponent<InputPlayer>();
        speed = Speed;
        DashReloadTime = ReloadDashTime;
        PlayerSprite = GetComponent<SpriteRenderer>();
        healthPlayer = GetComponent<PlayerHealth>();
        StartCoroutine(TimeBack());
        UIController.SetMaxTimeMovementReloadSlider(reloadAbility);
    }
    private void FixedUpdate()
    {
        isClean = true;
        MoveDash();
    }
    void Update()
    {
        MovePlayer();
        Dash();

        CleanInput();

        CheckCollisions();

        //MovePlayer();

        Jump();

        Attack();

        //Dash();

        BackTime();

        SlowMotion();

        anim.SetFloat(jumpHash, rigidBody.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }
    void CleanInput()
    {
        if (isClean)
        {
            speed		= Speed;
            isGrounded	= false;
            //isRunning	= false;
            isMove		= false;
            isRSideHit	= false;
            isLSideHit	= false;
        }
    }

  
    void CheckCollisions()
    {
        RaycastHit2D leftHit  = Raycast(new Vector2((GroundOffset.x + GroundOffsetPlus) * direction, GroundOffset.y), Vector2.down, length);
        RaycastHit2D rightHit = Raycast(new Vector2((-GroundOffset.x + GroundOffsetPlus) * direction, GroundOffset.y), Vector2.down, length);
        if (leftHit || rightHit) {isGrounded = true;  isDoubleJump = false; }
        isGroundUnderMe = Raycast(underGroudOffset, Vector2.down, underGroudLength);
		// Check side collision
        RaycastHit2D sideRHit = MyPhysics.BoxCast((Vector2)transform.position, new Vector2(-sideCheckOfsset.x, sideCheckOfsset.y), sideCheckSize, Vector2.right, sideJumpGridLayer );
		RaycastHit2D sideLHit = MyPhysics.BoxCast((Vector2)transform.position, new Vector2( sideCheckOfsset.x, sideCheckOfsset.y), sideCheckSize, Vector2.left, sideJumpGridLayer );
        //bool sideLHit = false;
        isRSideHit = sideRHit;
        isLSideHit = sideLHit;
    }
    void Rotate() {
        int localDirection = direction == -1 ? 0 : 1;
        transform.rotation = Quaternion.Euler(0, 180 * localDirection, 0);
    }
    void Rotate(int dir) {
        int localDirection = dir == -1 ? 0 : 1;
        transform.rotation = Quaternion.Euler(0, 180 * localDirection, 0);
    }
    float _BugTimeCheck = 0;
    void MovePlayer() // Move player and control move anim
    {
        if (isDashing) return;
        
        float moveX = PlayerInput.MoveX;
        bool isKey = (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow));
        if (isKey && _BugTimeCheck > (bugTimeCheck / 1000)) {
            moveX = 0;
        } else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) {
            _BugTimeCheck += Time.deltaTime;
        } else {
            _BugTimeCheck = 0;
        }
        if (!(isRSideHit || isLSideHit)) {
            if (moveX > 0) 
                direction = -1;
            else if (moveX < 0) 
                direction = 1;
        }

        if (Mathf.Abs (moveX) >= runOffset) {
            isMove = true;
        } else {
            isMove = false;
        }
        anim.SetFloat(runHash, moveX);
        if (moveX == 0 && lastMoveX == 0) {
            isRotate = false;
        } else if (Mathf.Abs(lastMoveX) - Mathf.Abs(moveX) >= runOffset) {
            isRotate = true;
        } else if (Mathf.Abs(moveX)  >= runOffset) {
            isRotate = false;
        }
        
        if (isMove || isRotate) {
            anim.SetFloat(runHash, Mathf.Abs(moveX));
            anim.SetBool(boolRunHash, true);
        } else {
            anim.SetBool(boolRunHash, false);
        }
        anim.SetBool("isSide", isRSideHit || isLSideHit);
        Rotate();
        if (isAttacking) moveX = 0;
        if (moveX != 0) {
            isMove = true;
        ///swordScript.EnableSword();
        }
        if (moveX != 0) {
            if (!isCanMove) {
                if (direction == -jumpMoveDir) {
                    //Debug.Log("Jump in right direction");
                    rigidBody.velocity = new Vector2(moveX * sideJumpMultipluyer * speed , rigidBody.velocity.y);
                } else {
                    //Debug.Log("JUmp in wrong direction");
                    rigidBody.AddForce(new Vector2(moveX * speed, -sideFallMultiplayer));
                    //rigidBody.velocity = new Vector2 (rigidBody.velocity.x + -Mathf.Sign(rigidBody.velocity.x) * Mathf.Abs(moveX) * speed * multiplayer, rigidBody.velocity.y);
                }
            } else {
                //Debug.Log("Move");
                rigidBody.velocity = new Vector2(moveX * speed, rigidBody.velocity.y);
            }
        } else if (isGrounded) {
            rigidBody.velocity = new Vector2(0,rigidBody.velocity.y);
        }
        //rigidBody.AddForce(new Vector2(moveX * speed * multiplaer - rigidBody.velocity.x, 0));
        lastMoveX = moveX;
    }

    void Jump()
    {
       
		/*
			Первый прыжок от земли
			Она касается стены
			Прыжок -  isAlreadyJump = true; isDoubleJump = false
			Она перестает касаться стены
			Она касается второй стены  isAlreadyJump = false
		*/
        
        KeyCode JumpButon = PlayerInput.GetJumpKeycode();
        //string JumpButon = "space";
        if (isGrounded && Input.GetKeyDown(JumpButon) )
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, JumpLength);
        }
        if ((isRSideHit || isLSideHit) && !isGrounded && Input.GetKeyDown(JumpButon)) {
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
            if ((isLSideHit || isRSideHit) && !isDrag) { // start drag on side hit
                isDrag = true;
                rigidBody.gravityScale = 1f;
                dragValue = 2;
            } else if (!(isLSideHit || isRSideHit)) { // if not side hit
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
        }
        if (isDrag && (rigidBody.velocity.y < 0)) { // Side drag
            dragValue -= Time.deltaTime * DragMultiplayer;
            direction = isLSideHit ? 1 : -1;
            float localDrag = Mathf.Clamp01( dragValue);
            rigidBody.drag = Mathf.Lerp(StartDragWhenSide, EndDragWhenSide, 1 - localDrag);
        } 
        if (!isGrounded && Input.GetKeyDown(JumpButon) && !isDoubleJump) { // Double jump
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, JumpLength);
            isDoubleJump = true;
        }
		//if (!isAlreadyJump && isRSideHit) {
		//	isCanJump = true;
		//}
		//Debug.Log("isCanJump" + isCanJump + " isAlreadyJump " + isAlreadyJump + " IsRSideHit " + isRSideHit);
    }
    IEnumerator WaitAfterJumpFunc () {
        isCanMove = false;
        yield return new WaitForSeconds(waitAfterJump);
        isCanMove = true;
    }
    void Attack()
    {
        bool isAttack = PlayerInput.isAttack;
        if (isAttack && TimingAttack > AttackDelay)
        {
            ////swordScript.DisableSword();
            bool res = SwordAtack.Action(Sword.position,SwordRadius, enemyLayer, 1,true);
            TimingAttack = 0;
            isAttacking = true;
            if (res) {
                reloadAbilityTime += addAbilityOnAttack;
            }
        }
        if (TimingAttack > AttackDelay  ) {
            isAttacking = false;
        }
        TimingAttack += Time.deltaTime;
    }
    void MoveDash() {
        float DashDistance   = 10f;
        if (isDashing) {
                anim.SetBool("isDash", true);
                Vector2 dist = DashSpeed * _dashDirection * DashDistance;
                direction = -(int)Mathf.Sign(_dashDirection.x);
                float localDirection = direction == -1 ? 0 : 1;
                //DashDistance = 10f;
                //float dist = DashSpeed * _dashDirection * DashDistance;
                
                GameObject _dashEffect =  Instantiate(dashEffect, transform.position, Quaternion.identity);
                _dashEffect.GetComponent<ParticleSystemRenderer>().flip = new Vector2(localDirection,0);
                Destroy(_dashEffect, 0.4f);
                rigidBody.velocity = dist;
                _dashTime -= Time.fixedDeltaTime;
                //rigidBody.AddForce(new Vector2(dist, 0), ForceMode2D.Impulse);
            }
        if (_dashTime <= 0 && isDashing) {
                anim.SetBool("isDash", false);
                isDashing = false;
                DashReloadTime = 0;
                _dashDirection = Vector2.zero;
                rigidBody.velocity = Vector2.zero;
            }
    }
    void Dash()
    {
        int isDash         = PlayerInput.isDash;
        if (DashReloadTime >= ReloadDashTime) {
            if (isDash != 0 && !isDashing && !isSlowMotionStarted) 
            {
                isDashing = true;
                _dashTime = dashTime;
                _dashDirection = new Vector2(isDash, 0);
                direction = -isDash;
                Rotate();
                
            } 
            if (isSlowMotionStarted && Input.GetMouseButton(0) && !isDashing) {
                isDashing = true;
                _dashTime = dashTime;
                _dashDirection = dashDirArrow.GetNormalizedRotation();
                //Debug.Log(_dashDirection);
                
                StopSlowMotion();
                //Debug.Log(localDir.x + " > " + dashDirArrow.transform.position.x + " " + (localDir.x > dashDirArrow.transform.position.x) + " Dir: " + direction);
                Rotate();
            }
        }
        else {
            DashReloadTime += Time.deltaTime;
        }
        UIController.DashReload(DashReloadTime / ReloadDashTime);
    }
    void StopSlowMotion() {
            dashDirArrow.SetActive(false);
            isSlowMotionStarted = false;
            _slowMotionEnd = 0;
    }
    void SlowMotion() {
        bool isStart =  PlayerInput.isSlowMotion;
        
        if (isStart && _slowMotionReloadTime >= slowMotionReloadTime && !isSlowMotionStarted) {
            dashDirArrow.SetActive(true);
            //Time.timeScale = changeToTimeScale;
            isSlowMotionStarted = true;
            _slowMotionReloadTime = 0;
        }
        if (isSlowMotionStarted && Time.timeScale > changeToTimeScale) {
            Time.timeScale = Mathf.Lerp(Time.timeScale, changeToTimeScale, Time.unscaledDeltaTime * slowMotionMultiplayer);
        } else if (!isSlowMotionStarted && Time.timeScale < 1) {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1, Time.unscaledDeltaTime * slowMotionMultiplayer * 2);
        }
        if (isSlowMotionStarted && _slowMotionEnd >= slowMotionEnd) {
            StopSlowMotion();
        }
        
        if (isSlowMotionStarted) {
            if (!(isRSideHit || isLSideHit) && !isDashing)  direction = -dashDirArrow.GetDirByAngle();
            _slowMotionEnd += Time.unscaledDeltaTime;
        }
        else 
            _slowMotionReloadTime += Time.deltaTime;
    }
    
    void BackTime() {
        if (reloadAbilityTime <= reloadAbility) {
            if (reloadAbilityEnable) 
            reloadAbilityTime += Time.deltaTime;
            UIController.SetTimeMovementReloadSlider(reloadAbilityTime);
            return;
        }
        bool back = PlayerInput.isBackTime;
        if (back) {
            reloadAbilityTime = 0;
            PlayerData dat = TimeMovement.GetTimeData();
            if (dat != null) {
                transform.position = dat.player_position;
                transform.rotation = dat.player_rotation;
                healthPlayer.SetHealth(dat.player_health);
                PlayerSprite.sprite = dat.player_anim;
            }
        }
    }

    
    IEnumerator TimeBack() {
        while (true) {
            yield return new WaitForSeconds(timeCatch);
            TimeMovement.SaveTimeData(transform,PlayerSprite.sprite,healthPlayer.GetHealth(),rigidBody.velocity.x, isGroundUnderMe);
        }

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
