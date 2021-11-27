using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageLine : MonoBehaviour
{
    private int lineDamage;

    public void Init(int damage)
    {
        lineDamage = damage;

        Invoke(nameof(OnDamage), 0.5f);
        Destroy(gameObject, 0.7f);
    }

    private void OnDamage()
    {
        //isAttack = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {

            if (other.CompareTag("Player"))
            {
                if(other.TryGetComponent(out PlayerObject playerObject))
                {
                    playerObject.DamageTo(lineDamage);
                }
            }
        
    }
}
