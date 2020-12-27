using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClassWait : EnemyClass
{
    public float waitBeforeAttack = 0.5f;
    bool _isWait;
    protected float _waitBeforeAttack;
    public void Update() {
        Wait();
        CheckAttack();
        
		if (!_isWait) 
        CheckTranslate();
        AfterUpdate();
        
    }
    protected override void AfterStart() {
        _isWait = false;
    }
    protected virtual void StartWait() {
        _isWait = true;
    }
    protected virtual void Wait() {
        if (_isWait)  _waitBeforeAttack += Time.deltaTime;
        else _waitBeforeAttack = 0;
        if (_waitBeforeAttack >= waitBeforeAttack) { 
            _isWait = false; 
            Attack(_playerHealth); 
            _waitBeforeAttack = 0;
        }
    }
    
}
