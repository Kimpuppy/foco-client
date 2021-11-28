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

        [SerializeField] private Collider2D thisCollider;


        public List<SpawnMonster> SpawnMonsters => spawnMonsters;

        protected override void Start() {
            base.Start();
            damage = 1;
        }

        protected override IEnumerator PlayPattern() {
            base.PlayPattern();
            
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
            _currentPattern.StopPattern();
            base.OnDead();
        }

        private void LateUpdate()
        {
            RotateSpawnCenter();
        }   
        
        protected override void OnPlayerAttackedToMonster(PlayerObject playerObject) 
        {
            DamageTo(playerObject.AttackDamage);
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && collision.contacts[0].otherCollider == thisCollider) 
            {
                PlayerObject playerObject = collision.gameObject.GetComponent<PlayerObject>();
                if (playerObject != null) {
                    OnCollisionToPlayer(playerObject);
                }
            }
        }

        private void RotateSpawnCenter()
        {
            spawnCenter.transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
        }
    }
}