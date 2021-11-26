using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : BaseObject {
    protected int hp;
    public int Hp => hp;
    
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
}
