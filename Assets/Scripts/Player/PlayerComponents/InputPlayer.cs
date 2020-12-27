using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerComp;
/// <summary>
/// This script control player input
/// </summary>
[RequireComponent(typeof(PlayerComponents))]
public class InputPlayer : MonoBehaviour, IUpdate
{
    [HideInInspector] 
    public bool isJumping;
    [HideInInspector]
    public bool isAttack;
    [HideInInspector]
    public bool isBackTime;
    [HideInInspector]
    public int isDash;
    [HideInInspector]
    public float MoveX;
    [HideInInspector]
    public bool isSlowMotion;
    
    [Header("PlayerControl")]
    public KeyCode Jump;
    public KeyCode Attack;
    public KeyCode Time_move;
    public KeyCode slowMotion;
    [Header("Joystick")]
    public KeyCode JoystickJump;
    public KeyCode JoystickAttack;
    public KeyCode JoystickTimeMove;
    public KeyCode joystickRightDash;
    public KeyCode joystickLeftDash;
    
    // Update is called once per frame
    public void DoUpdate()
    {
        // Update keyboard values
        isJumping = Input.GetKeyDown(Jump);
        isAttack = Input.GetKeyDown(Attack);
        MoveX = Input.GetAxis("Horizontal");
        bool isShift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        isDash = isShift ? (int)Input.GetAxisRaw("Dash") : 0;
        isBackTime = Input.GetKeyDown(Time_move);
        isSlowMotion = Input.GetKeyDown(slowMotion);
        if (Input.GetJoystickNames().Length > 0) {
            // Or if joystick connected, update joystick input
            isJumping = Input.GetKeyDown(JoystickJump);
            isAttack = Input.GetKeyDown(JoystickAttack);
            MoveX = Input.GetAxis("Horizontal");
            if (Input.GetKeyDown(joystickLeftDash)) {
                isDash = -1;
            } else if (Input.GetKeyDown(joystickRightDash)) {
                isDash = 1;
            } else { 
                isDash = 0;
            }
            isBackTime = Input.GetKeyDown(JoystickTimeMove);
            
        }
        /*
        if (Input.GetKeyDown(KeyCode.Joystick1Button0)) Debug.Log("0");
        else if (Input.GetKeyDown(KeyCode.Joystick1Button1)) Debug.Log("1");
        else if (Input.GetKeyDown(KeyCode.Joystick1Button2)) Debug.Log("2");
        else if (Input.GetKeyDown(KeyCode.Joystick1Button3)) Debug.Log("3");
        else if (Input.GetKeyDown(KeyCode.Joystick1Button4)) Debug.Log("4");
        else if (Input.GetKeyDown(KeyCode.Joystick1Button5)) Debug.Log("5");
        else if (Input.GetKeyDown(KeyCode.Joystick1Button6)) Debug.Log("6");
        else if (Input.GetKeyDown(KeyCode.Joystick1Button7)) Debug.Log("7");
        else if (Input.GetKeyDown(KeyCode.Joystick1Button8)) Debug.Log("8");
        else if (Input.GetKeyDown(KeyCode.Joystick1Button9)) Debug.Log("9");
        else if (Input.GetKeyDown(KeyCode.Joystick1Button10)) Debug.Log("10");
        else if (Input.GetKeyDown(KeyCode.Joystick1Button11)) Debug.Log("11");
        */


    }
    public KeyCode GetJumpKeycode() {
        return Input.GetJoystickNames().Length > 0 ? JoystickJump : Jump;
    }
    
}
