using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Jaeyun.SpawnBoss
{
    [CreateAssetMenu(menuName = "Spawn")]
    public class SpawnPattern : Pattern
    {
        public float moveSpeed;

        public Pattern[] patterns;
        private SpawnBoss _spawnBoss;

        public override void EnterPattern()
        {
            _thisPatternRoutine = _boss.StartCoroutine(MoveToCenter());
        }

        private IEnumerator MoveToCenter()
        {
            var dist = ((Vector2)_boss.transform.position).magnitude;
            var time = dist / moveSpeed;
            _boss.transform.DOMove(Vector2.zero, time);

            yield return new WaitForSeconds(time);
            _spawnBoss = (SpawnBoss)_boss;
            _spawnBoss.ReSpawn();
            
            ExitPattern();
        }

        public override void ExitPattern()
        {
            var index = Random.Range(0, patterns.Length);
            _spawnBoss.StartPattern(patterns[index]);
        }

        protected override void OnStopPattern()
        {
            
        }
    }
}