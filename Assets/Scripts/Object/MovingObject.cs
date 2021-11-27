using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Object 생성 후 Info로 Init함
// ex) object.Init(new MovingObjectInfo {
//     Hp = 10,
// })
public class MovingObjectInfo : ObjectInfo{
    public int Hp;
}

public class MovingObject : BaseObject {
    [SerializeField]
    protected int hp;
    public int Hp => hp;
    
    protected int maxHp;
    public int MaxHp => maxHp;
    
    public override void Init(ObjectInfo objectInfo) {
        base.Init(objectInfo);
        
        if (objectInfo is MovingObjectInfo movingObjectInfo) {
            hp = movingObjectInfo.Hp;
        }
    }
    
    protected override void Start() {
        base.Start();
        
        maxHp = hp;
    }
    
    protected override void Update() {
        base.Update();
    }

    public virtual void DamageTo(int damage) {
        hp -= damage;
        if (hp <= 0) {
            OnDead();
        }
        
        OnDamaged();
    }

    protected virtual void OnDamaged() {
    }

    protected virtual void OnDead() {
    }
    
    public virtual void HealTo(int heal) {
        hp += heal;
        if (hp >= maxHp) {
            hp = maxHp;
        }
        
        OnHeal();
    }
    
    protected virtual void OnHeal() {
    }
}
