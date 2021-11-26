using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MovingObject {
    private float velocity = 0.0f;
    public float Velocity => velocity;
    
    protected virtual void Update() {
        base.Update();
        velocity = rigidbody.velocity.magnitude;
    }
}
