using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Jaeyun.SpawnBoss
{
    
    [RequireComponent(typeof(AnimationPlayer))]
    public class SpawnBoss : BossObject
    {
        [SerializeField] private AnimationPlayer animationPlayer;

        [SerializeField]
        private Transform spawnCenter;
        [SerializeField]
        private List<SpawnMonster> spawnMonsters;

        [SerializeField] private float rotateTime;

        [SerializeField] private Pattern startPattern;
        private Pattern _currentPattern;

        public int needRespawnCount = 3;

        public float rotateSpeed;

        public int ActiveMonsterCount => spawnMonsters.Count(monster => monster.IsActive); 
        public bool IsNeedToSpawn =>  ActiveMonsterCount <= needRespawnCount;


        public List<SpawnMonster> SpawnMonsters => spawnMonsters;
        
        protected override IEnumerator PlayPattern()
        {
            StartPattern(startPattern);
            
            yield return null;
        }

        public void StartPattern(Pattern pattern)
        {
            pattern.Init(this, animationPlayer);
            pattern.EnterPattern();
            _currentPattern = pattern;
        }

        public void ReSpawn()
        {
            spawnMonsters.ForEach(spawnMonster => spawnMonster.Activate());
        }

        protected override void OnDead()
        {
            base.OnDead();
            _currentPattern.StopPattern();
            Destroy(gameObject);
        }

        private void LateUpdate()
        {
            RotateSpawnCenter();
        }

        private void RotateSpawnCenter()
        {
            spawnCenter.transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
        }
    }
}