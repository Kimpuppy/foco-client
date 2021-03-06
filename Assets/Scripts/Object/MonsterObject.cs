using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterObjectInfo : MovingObjectInfo {
    public int Damage;
}

public class MonsterObject : MovingObject {
    protected int damage = 0;
    public int Damage => damage;
    
    public override void Init(ObjectInfo objectInfo) {
        base.Init(objectInfo);
        
        if (objectInfo is MonsterObjectInfo monsterObjectInfo) {
            damage = monsterObjectInfo.Damage;
        }
    }
    
    protected override void Start() {
        base.Start();
    }
    
    protected override void Update() {
        base.Update();
    }
    
    protected override void OnDamaged() {
        base.OnDamaged();
    }
    
    protected override void OnDead() {
        base.OnDead();
    }
    
    protected virtual void OnPlayerAttackedToMonster(PlayerObject playerObject) {
    }
    
    protected virtual void OnPlayerDamagedByMonster(PlayerObject playerObject) {
        //playerObject.DamageTo(damage);
    }
    
    protected virtual void OnCollisionToPlayer(PlayerObject playerObject) {
        if (playerObject.IsAttacking) {
            OnPlayerAttackedToMonster(playerObject);
        }
        else {
            OnPlayerDamagedByMonster(playerObject);
        }
    }
    
    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            PlayerObject playerObject = collision.gameObject.GetComponent<PlayerObject>();
            if (playerObject != null) {
                OnCollisionToPlayer(playerObject);
            }
        }
    }
}
