using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class IronInfo : MonsterObjectInfo 
{
    public float FallSpeed;
}
public class Iron : MonoBehaviour
{
    private float fallSpeed = 4;

    //public override void Init(ObjectInfo objectInfo)
    //{
    //    base.Init(objectInfo);
    //    
    //    if (objectInfo is IronInfo ironInfo) 
    //    {
    //        fallSpeed = ironInfo.FallSpeed;
    //        damage = 2;
    //    }
    //    
    //    Destroy(gameObject, 10f);
    //}

    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    private void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if(other.transform.TryGetComponent(out PlayerObject playerObject))
            {
                playerObject.DamageTo(1);
                Destroy(gameObject);
            }
        }
    }
}