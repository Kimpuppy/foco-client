using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class IronInfo : MonsterObjectInfo {
    public float FallSpeed;
}

public class Iron : MonsterObject
{
    private float fallSpeed;

    public override void Init(ObjectInfo objectInfo) {
        base.Init(objectInfo);
        
        if (objectInfo is IronInfo ironInfo) {
            fallSpeed = ironInfo.FallSpeed;
        }
        
        Destroy(gameObject, 1.5f);
    }

    private void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}