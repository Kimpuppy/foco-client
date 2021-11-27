using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MovingObject {
    private Vector2 clickPos = Vector2.zero;
    private Vector2 dragPos = Vector2.zero;
    protected float dragPower = 0;
    
    private float velocity = 0.0f;
    public float Velocity => velocity;
    
    private bool isAttacking = false;
    public bool IsAttacking => isAttacking;
    
    protected virtual void Update() {
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
}
