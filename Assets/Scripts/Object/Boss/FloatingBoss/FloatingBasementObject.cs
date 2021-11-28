using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBasementObject : MonsterObject {
    public override void Init(ObjectInfo objectInfo) {
        base.Init(objectInfo);
    }
    
    protected override void Start() {
        base.Start();
        damage = 1;
        Destroy(gameObject, 2.0f);
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            PlayerObject playerObject = other.GetComponent<PlayerObject>();
            if (playerObject != null) {
                playerObject.DamageTo(damage);
            }
        }
    }
}