using System;
using UnityEngine;

namespace Jaeyun
{
    
    [RequireComponent(typeof(CircleCollider2D))]
    public class WeakPoint : MonoBehaviour
    {
        [SerializeField] private WeakPointBoss boss;

        private Action<WeakPoint> _onWeakPointAttacked;

        private void Start()
        {
            DeActivate();
        }

        public void Activate(Action<WeakPoint> onWeakPointAttacked)
        {
            gameObject.SetActive(true);
            _onWeakPointAttacked = onWeakPointAttacked;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                WeakPointAttacked();
            }   
        }

        private void WeakPointAttacked()
        {
            boss.WeakPointAttack();
            _onWeakPointAttacked?.Invoke(this);
            DeActivate();
        }

        public void DeActivate()
        {
            gameObject.SetActive(false);
        }
    }
}