using System;
using UnityEngine;

namespace Jaeyun
{
    public class ShootBeam : MonoBehaviour
    {
        public float shootPower;
        public Rigidbody2D rigidbody2D;

        public int damage;

        public void Shoot()
        {
            var player = GameWorld.Instance.WorldPlayerObject;
            if (player == null)
            {
                player = FindObjectOfType<PlayerObject>();
            }

            rigidbody2D.velocity = Vector2.zero;
            
            Vector2 dir = player.transform.position - transform.position;

            rigidbody2D.AddForce(dir.normalized * shootPower, ForceMode2D.Impulse);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var player = GameWorld.Instance.WorldPlayerObject;
                player.DamageTo(damage);
            }
        }
    }
}