using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfineLine : MonsterObject
{
    protected override void Start()
    {
        base.Start();
        
        Init(new MovingObjectInfo() { Hp = 15 });
    }

    protected override void Init(MovingObjectInfo movingObjectInfo)
    {
        base.Init(movingObjectInfo);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    public void InActive()
    {
        
    }
    
    public void OnActive()
    {
        
    }
}
