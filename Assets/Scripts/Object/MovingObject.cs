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
    
    protected virtual void Init(ObjectInfo objectInfo) {
        base.Init(objectInfo);
        
        if (objectInfo is MovingObjectInfo movingObjectInfo) {
            hp = movingObjectInfo.Hp;
        }
    }
    
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
