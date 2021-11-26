using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterObject : MovingObject {
    protected virtual void Start() {
        base.Start();
    }
    
    protected virtual void Update() {
        base.Update();
    }

    public void DamageTo(int damage) {
        hp -= damage;
        OnDamaged();
        
        if (hp <= 0) {
            OnDead();
        }
    }

    protected virtual void OnDamaged() {
        
    }
    
    protected virtual void OnDead() {
        
    }

    protected virtual void OnPlayerAttacked() {
        
    }
    
    protected virtual void OnPlayerDamaged() {
        
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            PlayerObject playerObject = collision.gameObject.GetComponent<PlayerObject>();
            if (playerObject != null) {
                if (playerObject.Velocity > GameConstants.PLAYER_ATTACK_VELOCITY) {
                    OnPlayerAttacked();
                }
                else {
                    OnPlayerDamaged();
                }
            }
        }
    }
}
