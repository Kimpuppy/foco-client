using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iron : MonsterObject
{
    private float fallSpeed;

    protected override void Init(MovingObjectInfo movingObjectInfo)
    {
        base.Init(movingObjectInfo);
    }

    public void Active(float speed)
    {
        Init(new MonsterObjectInfo() {Damage = 2});
        fallSpeed = speed;
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