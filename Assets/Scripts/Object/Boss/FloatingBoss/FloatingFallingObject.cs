using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingFallingObject : MonsterObject {
    public float MoveSpeed;
    
    public override void Init(ObjectInfo objectInfo) {
        base.Init(objectInfo);
    }
    
    protected override void Start() {
        base.Start();
        damage = 1;
        StartCoroutine(Move());
        Destroy(gameObject, 5.0f);
    }
    
    private IEnumerator Move() {
        while (true) {
            transform.Translate(Vector3.down * MoveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            PlayerObject playerObject = other.GetComponent<PlayerObject>();
            if (playerObject != null) {
                playerObject.DamageTo(damage);
                Destroy(gameObject);
            }
        }
    }
}