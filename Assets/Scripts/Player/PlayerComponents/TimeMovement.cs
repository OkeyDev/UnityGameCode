using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerComp;
// Script is not using
[RequireComponent(typeof(PlayerComponents))]
public class TimeMovement : MonoBehaviour, IUpdate, IStart
{
    public float timeCatch;
    public float reloadAbility;
    PlayerComponents playerComponents;
    InputPlayer playerInput;
    SpriteRenderer PlayerSprite;
    PlayerHealth healthPlayer;
    Rigidbody2D rigidBody;
    float reloadAbilityTime = 0f;
    static PlayerData[] timeData = new PlayerData[30];
    //static int[] timeData = new int[30];
    public void DoStart() {
        playerComponents = GetComponent<PlayerComponents>();
        playerInput = playerComponents.inputPlayerScript;
        PlayerSprite = GetComponent<SpriteRenderer>();
        rigidBody = playerComponents.rigidbody2d;
        healthPlayer = GetComponent<PlayerHealth>();
        reloadAbilityTime = reloadAbility;
        UIController.SetMaxTimeMovementReloadSlider(reloadAbility);
        StartCoroutine(TimeBack());

    }
    public void DoUpdate() {
        if (reloadAbilityTime <= reloadAbility) {
            reloadAbilityTime += Time.deltaTime;
            UIController.SetTimeMovementReloadSlider(reloadAbilityTime);
            return;
        }
        bool back = playerInput.isBackTime;
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
            bool isGroundUnderMe = playerComponents.raycasts.isGroundUnderMe;
            yield return new WaitForSeconds(timeCatch);
            TimeMovement.SaveTimeData(transform,PlayerSprite.sprite,healthPlayer.GetHealth(),rigidBody.velocity.x, isGroundUnderMe);
        }

    }
    public static void SaveTimeData(Transform transform, Sprite sprite, int health, float rigidbody_X, bool isSave=false) {
        for (int i = timeData.Length - 1;i > 0;i--) {
            timeData[i] = timeData[i - 1];
        }
        //Debug.Log(timeData[timeData.Length - 1].player_transform.position );
        if (isSave) timeData[0] = new PlayerData(transform,sprite,health,rigidbody_X);
        /*
        PlayerData lastElement = timeData[timeData.Length - 1];
       
        if (lastElement == null) {
            //Debug.Log("Null");
            return;
        }
        Debug.Log("ClonePos: " + lastElement.player_transform.position + " 0: " + timeData[0].player_transform.position + " 1: " + timeData[1].player_transform.position);
        playerCopy.SetData(lastElement.player_position,lastElement.player_rotation,lastElement.player_anim);
        */
    }
    public static void SaveTimeData(Transform transform, Sprite sprite, int health) {
        SaveTimeData(transform,sprite,health,0);
    }
    public static PlayerData GetTimeData() {
        PlayerData lastElement = null;
        for (int i = timeData.Length - 1;i >= 0;i--) {
            if (timeData[i] != null && lastElement == null) {
                lastElement = timeData[i];
            }
            timeData[i] = null;
        }
        return lastElement;
    }
}
