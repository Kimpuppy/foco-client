using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : BaseObject {
    protected int hp;
    public int Hp => hp;
    
    protected override void Start() {
        base.Start();
    }
    
    protected override void Update() {
        base.Update();
    }

    public virtual void DamageTo(int damage) {
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
