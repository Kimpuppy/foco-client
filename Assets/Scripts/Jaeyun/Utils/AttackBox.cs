using System;
using UnityEngine;

namespace Jaeyun
{
    [RequireComponent(typeof(Collider2D))]
    public class AttackBox : MonoBehaviour
    {
        [SerializeField]
        private int damage;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Damage");
                GameWorld.Instance.WorldPlayerObject?.DamageTo(damage);
            }
        }
    }
}