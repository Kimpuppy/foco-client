using System;
using UnityEngine;

namespace Jaeyun
{
    
    [RequireComponent(typeof(CircleCollider2D))]
    public class WeakPoint : MonoBehaviour
    {

        [SerializeField] private BossObject boss;
        
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                
            }   
        }
    }
}