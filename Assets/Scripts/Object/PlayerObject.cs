using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectInfo : MovingObjectInfo {
    public int AttackDamage;
}

public class PlayerObject : MovingObject {
    private Vector2 clickPos = Vector2.zero;
    private Vector2 dragPos = Vector2.zero;
    protected float dragPower = 0;
    
    private float velocity = 0.0f;
    public float Velocity => velocity;
    
    private bool isAttacking = false;
    public bool IsAttacking => isAttacking;
    
    private int attackDamage = 1;
    public int AttackDamage => attackDamage;
    
    public override void Init(ObjectInfo objectInfo) {
        base.Init(objectInfo);
        
        if (objectInfo is PlayerObjectInfo playerObjectInfo) {
            attackDamage = playerObjectInfo.AttackDamage;
        }
    }
    
    protected override void Start() {
        base.Start();
    }
    
    protected override void Update() {
        base.Update();
        
        Move();
        
        velocity = rigidbody.velocity.magnitude;
        isAttacking = (velocity > GameConstants.PLAYER_ATTACK_VELOCITY) ? true : false;
    }
    
    private void Move() {
        if (Input.GetMouseButtonDown(0)) {
            clickPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0)) {
            dragPos = Input.mousePosition;

            dragPower = Vector2.Distance(clickPos, dragPos) * 0.05f;
        }
        if (Input.GetMouseButtonUp(0)) {
            Vector2 dragDirection = (clickPos - dragPos).normalized;
            rigidbody.AddForce(dragDirection * dragPower, ForceMode2D.Impulse);
            
            dragPower = 0;
            clickPos = Vector2.zero;
            dragPos = Vector2.zero;
        }
    }
    
    public override void DamageTo(int damage) {
        base.DamageTo(damage);
    }
    
    protected override void OnDamaged() {
        base.OnDamaged();
    }
    
    protected override void OnDead() {
        base.OnDead();
        //Destroy(gameObject);
    }
    
    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Monster")) {
            MonsterObject monsterObject = collision.gameObject.GetComponent<MonsterObject>();
            if (monsterObject != null) {
                if (isAttacking) {
                    monsterObject.DamageTo(attackDamage);
                }
            }
        }
    }
}
