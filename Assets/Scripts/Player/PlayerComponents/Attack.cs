using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerComp;
//Delete it
[RequireComponent(typeof(PlayerComponents))]
public class Attack : MonoBehaviour, IUpdate, IStart
{
    public Transform swordPosition;
    public float swordRadius;
    public float attackDelay;
    public float addAbilityOnAttack;
    public LayerMask enemyLayer;
    PlayerComponents playerComponents;
    InputPlayer playerInput;
    float timingAttack;
    float reloadAbilityTime;
    [HideInInspector]
    public bool isAttacking;
    // Start is called before the first frame update
    public void DoStart()
    {
        playerComponents = gameObject.GetComponent<PlayerComponents>();
        playerInput = playerComponents.inputPlayerScript;
    }

    // Update is called once per frame
    public void DoUpdate()
    {
        bool isAttack = playerInput.isAttack;
        if (isAttack && timingAttack > attackDelay)
        {
            ////swordScript.DisableSword();
            bool res = SwordAtack.Action(swordPosition.position,swordRadius, enemyLayer, 1,true);
            timingAttack = 0;
            isAttacking = true;
            if (res) {
                reloadAbilityTime += addAbilityOnAttack;
            }
        }
        if (timingAttack > attackDelay  ) {
            isAttacking = false;
        }
        timingAttack += Time.deltaTime;
    }
}
