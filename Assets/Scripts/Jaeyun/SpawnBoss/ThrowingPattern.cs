using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Jaeyun.SpawnBoss
{
    [CreateAssetMenu(menuName = "Throwing")]
    public class ThrowingPattern : Pattern
    {
        
        private List<SpawnMonster> _spawnMonsters;
        private SpawnBoss _spawnBoss;

        private int _throwingCount;

        [SerializeField] private float throwingSpeed;
        [SerializeField] private float throwingDelay;
        
        [SerializeField] private Pattern spawnPattern;
        [SerializeField] private Pattern anotherAttackPattern;
        
        public override void EnterPattern()
        {
            _spawnBoss = _boss as SpawnBoss;
            _spawnMonsters = _spawnBoss.SpawnMonsters;
            _thisPatternRoutine = _boss.StartCoroutine(ThrowingRoutine());
        }

        IEnumerator ThrowingRoutine()
        {
            _throwingCount = 0;
            for (int i = 0; i < _spawnBoss.ActiveMonsterCount; i++)
            {
                yield return new WaitForSeconds(throwingDelay);
                Throwing();    
            }
            ExitPattern();
        }

        private void Throwing()
        {
            Debug.Log("Throwing!");
            var minDist = float.MaxValue;
            var player = FindObjectOfType<PlayerObject>();

            SpawnMonster minDistMonster = null; 
            
            for (int i = 0; i < _spawnMonsters.Count; i++)
            {
                var targetMonster = _spawnMonsters[i];
                if(!targetMonster.IsActive) continue;

                var dist = Vector2.Distance(player.transform.position, 
                    targetMonster.transform.position);

                if (dist < minDist)
                {
                    minDist = dist;
                    minDistMonster = targetMonster;
                }
            }

            if (minDistMonster != null)
            {
                var targetPos = player.transform.position;
                var duration = minDist / throwingSpeed;
                var originPos = minDistMonster.transform.localPosition;
                var goMove = minDistMonster.transform.DOMove(targetPos, duration);
                goMove.onComplete += () =>
                {
                    minDistMonster.transform.DOLocalMove(originPos, duration);
                };
            }
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