using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerComp;
/// <summary>
/// Enable slow motion, also access use direction dash
/// </summary>
[RequireComponent(typeof(PlayerComponents))]
public class SlowMotion : MonoBehaviour, IUpdate, IStart
{
    public ArrowDirection dashDirArrow;
    public float changeToTimeScale; 
    public float slowMotionMultiplayer = 2f;
    public float slowMotionReloadTime;
    public float slowMotionEnd;
    private float _slowMotionEnd;
    private float _slowMotionReloadTime;
    [HideInInspector]
    public bool isSlowMotionStarted;
    PlayerComponents playerComponents;
    InputPlayer playerInput;
    Raycasts raycasts;
    PlayerDirection playerDirection;
    // Start is called before the first frame update
    public void DoStart()
    {
        playerComponents = GetComponent<PlayerComponents>();
        playerInput = playerComponents.inputPlayerScript;
        playerDirection = playerComponents.playerDirection;
        raycasts = playerComponents.raycasts;
    }

    // Update is called once per frame
    public void DoUpdate()
    {
        SlowMotionU();
    }
    public void StopSlowMotion() {
            dashDirArrow.SetActive(false);
            isSlowMotionStarted = false;
            _slowMotionEnd = 0;
    }
    void SlowMotionU() {
        bool isStart =  playerInput.isSlowMotion;
        ref bool isSideHit = ref raycasts.isSideHit;
        ref bool isDashing = ref playerComponents.dashScript.isDashing;
        if (isStart && _slowMotionReloadTime >= slowMotionReloadTime && !isSlowMotionStarted) {
            dashDirArrow.SetActive(true);
            //Time.timeScale = changeToTimeScale;
            isSlowMotionStarted = true;
            _slowMotionReloadTime = 0;
        }
        if (isSlowMotionStarted && Time.timeScale > changeToTimeScale) {
            Time.timeScale = Mathf.Lerp(Time.timeScale, changeToTimeScale, Time.unscaledDeltaTime * slowMotionMultiplayer);
        } else if (!isSlowMotionStarted && Time.timeScale < 1 && Time.timeScale != 0) {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1, Time.unscaledDeltaTime * slowMotionMultiplayer * 2);
        }
        if (isSlowMotionStarted && _slowMotionEnd >= slowMotionEnd) {
            StopSlowMotion();
        }
        
        if (isSlowMotionStarted) {
            if (!isSideHit && !isDashing)  playerDirection.direction = -dashDirArrow.GetDirByAngle();
            _slowMotionEnd += Time.unscaledDeltaTime;
        }
        else 
            _slowMotionReloadTime += Time.deltaTime;
    }
}
