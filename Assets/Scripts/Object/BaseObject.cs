using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 오브젝트에 포함되어야할 것 -> Rigidbody, OnCollisionEnter
// MovingObject인것들 -> 플레이어, 몬스터, 데미지를 주면 파괴되는 오브젝트 / OnDead(), OnDamaged(), Hp
// MovingObject가 아닌것들 -> 아이템, 이펙트, 스킬

public class ObjectInfo {
}

public class BaseObject : MonoBehaviour {
    protected Rigidbody2D rigidbody;
    public Rigidbody2D Rigidbody => rigidbody;
    
    public virtual void Init(ObjectInfo objectInfo) {
    }
    
    protected virtual void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    
    protected virtual void Update() {
    }
}
