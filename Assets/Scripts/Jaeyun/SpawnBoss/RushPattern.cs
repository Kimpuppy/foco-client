

using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Jaeyun.SpawnBoss
{
    [CreateAssetMenu(menuName = "Pattern/Rush")]
    public class RushPattern : Pattern
    {
        private Rigidbody2D _rigidbody;

        [SerializeField] private float rushSpeed;
        [SerializeField] private int rushCount;
        [SerializeField] private float rushDelay;

        private SpawnBoss _spawnBoss;

        [SerializeField] private Pattern spawnPattern;
        [SerializeField] private Pattern anotherAttackPattern;
        
        
        public override void EnterPattern()
        {
            _spawnBoss = _boss as SpawnBoss;
            _rigidbody = _boss.GetComponent<Rigidbody2D>();
            _thisPatternRoutine = _boss.StartCoroutine(RushRoutine());
        }

        IEnumerator RushRoutine()
        {
            yield return new WaitForSeconds(rushDelay);
            for (int i = 0; i < rushCount; i++)
            {
                Rush();
                yield return new WaitForSeconds(rushDelay);
            }
        }
        
        private void Rush()
        {
            var playerObject = GameWorld.Instance.WorldPlayerObject;
            if (playerObject == null)
            {
                playerObject = FindObjectOfType<PlayerObject>();
            }
            var targetPos = playerObject.transform.position;

            Vector2 vectorValue = targetPos - _boss.transform.position;
            var dist = vectorValue.magnitude;
            var dir = vectorValue.normalized;

            _rigidbody.AddForce(dir * dist * rushSpeed, ForceMode2D.Force); 
        }

        public override void ExitPattern()
        {
            if (_spawnBoss.IsNeedToSpawn)
            {
                _spawnBoss.StartPattern(spawnPattern);
            }
            else
            {
                _spawnBoss.StartPattern(anotherAttackPattern);
            }
        }

        protected override void OnStopPattern()
        {
            
        }
    }
}